using AutoMapper;
using Newtonsoft.Json;
using SurveyMonkey.Business.Extensions;
using SurveyMonkey.Business.Helper;
using SurveyMonkey.Business.IServices;
using SurveyMonkey.DataAccess.IRepos;
using SurveyMonkey.DataTransferObject.Request;
using SurveyMonkey.DataTransferObject.Response;
using SurveyMonkey.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SurveyMonkey.Business.Services
{
    public class SurveyReportService : ISurveyReportService
    {
        private readonly ISurveyRepo _repo;
        private readonly IAnswerRepo _answerRepo;
        private readonly ILineAnswerRepo _lineAnswerRepo;
        private readonly ISingleChoiceRepo _singleRepo;
        private readonly IMultiChoiceRepo _multiRepo;
        private readonly IMapper _mapper;

        public SurveyReportService(ISurveyRepo repo, IAnswerRepo answerRepo, ILineAnswerRepo lineAnswerRepo, ISingleChoiceRepo singleRepo, IMultiChoiceRepo multiRepo, IMapper mapper)
        {
            _repo = repo;
            _answerRepo = answerRepo;
            _lineAnswerRepo = lineAnswerRepo;
            _singleRepo = singleRepo;
            _multiRepo = multiRepo;
            _mapper = mapper;
        }
        public async Task<IEnumerable<QuestionLineAnswerReportResponse>> GetLineAnswerReport(int id, string email)
        {
            Survey survey = await _repo.GetByIdForReportAsync(id, email);
            IEnumerable<QuestionLineAnswerReportResponse> report = await getQuestionLineAnswerReport(survey);
            return report;
        }

        public async Task<SurveyReportResponse> GetReportAsync(int id, string userMail)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            Survey item = await _repo.GetByIdForReportAsync(id, userMail);
            if (item == default)
            {
                return default;
            }
            SurveyReportResponse report = await generateReport(item);
            stopwatch.Stop();
            report.Stopwatch = stopwatch;
            return report;
        }

        public async Task<MemoryStream> GetLineAnswerReportMemStream(int id, string email)
        {
            Survey survey = await _repo.GetByIdForReportAsync(id, email);
            IEnumerable<QuestionLineAnswerReportResponse> data = await getQuestionLineAnswerReport(survey);
            var jsonData = JsonConvert.SerializeObject(data);
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(jsonData);
            var content = new MemoryStream(bytes);
            return content;
        }


        private async Task<IEnumerable<QuestionLineAnswerReportResponse>> getQuestionLineAnswerReport(Survey survey)
        {
            ICollection<QuestionLineAnswerReportResponse> report = new List<QuestionLineAnswerReportResponse>();
            foreach (var question in survey.Questions)
            {
                if (question.QuestionTypeId == QuestionTypes.SingleLine || question.QuestionTypeId == QuestionTypes.MultiLine)
                {
                    var answers = await _lineAnswerRepo.LineAnswersForReport(question.Id);
                    var item = new QuestionLineAnswerReportResponse
                    {
                        Text = question.Text,
                        lineAnswers = answers.ConvertToVirtualDto<LineAnswerView>(_mapper)
                    };
                    report.Add(item);
                }
            }
            return report;
        }



        private async Task<int> getParticipantForGenerateReport(int id)
        {
            var count = await _answerRepo.GetCountParticipant(id);
            return count;
        }
        private async Task<IList<SurveyReportQuestionView>> getQuestionsForGenerateReport(Survey item)
        {
            SurveyReportChoicesView choicesView;
            SurveyReportQuestionView questionView;
            List<SurveyReportQuestionView> questionList = new();
            foreach (var question in item.Questions)
            {
                questionView = new SurveyReportQuestionView
                {
                    QuestionId = question.Id,
                    Text = question.Text,
                };

                foreach (var choices in question.Choices)
                {

                    choicesView = new SurveyReportChoicesView
                    {
                        ChoiceId = choices.Id,
                        Text = choices.Text,
                        Count = await countToAnswers(choices.Id, question.QuestionTypeId)
                    };

                    questionView.Choices.Add(choicesView);
                }
                questionList.Add(questionView);
            }
            return questionList;
        }
        private async Task<int> countToAnswers(int choiceId, int questionType)
        {
            if (questionType == QuestionTypes.SingleChoice || questionType == QuestionTypes.Rating)
            {
                var count = await _singleRepo.GetCountChoice(choiceId);
                return count;
            }
            else if (questionType == QuestionTypes.MultiChoice)
            {
                var count = await _multiRepo.GetCountChoice(choiceId);
                return count;
            }
            return 0;
        }
        private async Task<SurveyReportResponse> generateReport(Survey item)
        {
            var survey = new SurveyReportResponse
            {
                SurveyId = item.Id,
                Participant = await getParticipantForGenerateReport(item.Id),
                SurveyName = item.Name,
                Questions = await getQuestionsForGenerateReport(item)
            };
            return survey;
        }

        
    }
}
