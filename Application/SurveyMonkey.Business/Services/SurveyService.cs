using AutoMapper;
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

namespace SurveyMonkey.Business.Services
{
    public class SurveyService : ISurveyService
    {
        private readonly ISurveyRepo _repo;
        private readonly IAnswerRepo _answerRepo;
        private readonly ILineAnswerRepo _lineAnswerRepo;
        private readonly ISingleChoiceRepo _singleRepo;
        private readonly IMultiChoiceRepo _multiRepo;
        private readonly IMapper _mapper;

        public SurveyService(ISurveyRepo repo, IAnswerRepo answerRepo, ILineAnswerRepo lineAnswerRepo, ISingleChoiceRepo singleRepo, IMultiChoiceRepo multiRepo, IMapper mapper)
        {
            _repo = repo;
            _answerRepo = answerRepo;
            _lineAnswerRepo = lineAnswerRepo;
            _singleRepo = singleRepo;
            _multiRepo = multiRepo;
            _mapper = mapper;
        }

        public async Task<int> CreateSurveyAsync(SurveyCreateRequest survey)
        {
            foreach (var question in survey.Questions)
            {
                if (question.QuestionTypeId == 3)
                {
                    question.Choices = new List<ChoiceForSurveyCreate>();
                    for (int i = 1; i < 11; i++)
                    {
                        question.Choices.Add(new() { Text = i.ToString() });
                    }
                }
            }
            var item = _mapper.Map<Survey>(survey);
            
            await _repo.CreateAsync(item);
            return item.Id;
        }

        public async Task<SurveyReportResponse> GetReportAsync(int id, string userMail)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            Survey item = await _repo.GetByIdForReportAsync(id,userMail);
            SurveyReportResponse report = await generateReport(item);
            stopwatch.Stop();
            report.Stopwatch = stopwatch;
            return report;
        }

       
        public async Task AddAnswer(AnswerRequest answer)
        {
            var survey = await _repo.GetSurveyForAddAnswerControl(answer.SurveyId);
            if (await controlSurveyForAnswer(survey,answer))
            {
                Answer _answer  = answer.ConvertToEntity<Answer>(_mapper);
                await _repo.AddAnswerToSurvey(_answer);
            }
            else
            {
                throw new Exception(message: "anket, soru veya seçim id'si hatalı");
            }

        }

        public async Task<SurveyResponse> GetSurveyByIdAsync(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            return item.ConvertToDto<SurveyResponse>(_mapper);
        }
        public async Task<IEnumerable<QuestionLineAnswerReportResponse>> GetLineAnswerReport(int id,string email)
        {
            Survey survey = await _repo.GetByIdForReportAsync(id,email);
            IEnumerable<QuestionLineAnswerReportResponse> report = await getQuestionLineAnswerReport(survey);
            return report;
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
                        Id = question.Id,
                        Text = question.Text,
                        lineAnswers =answers.ConvertToVirtualDto<LineAnswerView>(_mapper)
                    };
                    report.Add(item);   
                }
            }
            return report;
        }

        private async Task<bool> controlSurveyForAnswer(Survey survey, AnswerRequest answer)
        {
            foreach (var singleChoiceAnswer in answer.SingleChoiceAnswer)
            {
                var questionAndChoiceControl = survey.Questions.Where(q => q.Id == singleChoiceAnswer.QuestionId).FirstOrDefault()?.Choices.Where(c => c.Id == singleChoiceAnswer.ChoiceId).Count() != 1;
                if (questionAndChoiceControl)
                {
                    return false;
                }
            }
            foreach (var multiChoiceAnswer in answer.MultiChoiceAnswer)
            {
                var questionAndChoiceControl = survey.Questions.Where(q => q.Id == multiChoiceAnswer.QuestionId).FirstOrDefault()?.Choices.Where(c => c.Id == multiChoiceAnswer.ChoiceId).Count() != 1;
                if (questionAndChoiceControl)
                {
                    return false;
                }
            }
            foreach (var lineAnswers in answer.lineAnswers)
            {
                var questionControl = survey.Questions.Where(q => q.Id == lineAnswers.QuestionId).Count() != 1;
                if (questionControl)
                {
                    return false;
                }
            }
            return true;
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
            if (questionType == QuestionTypes.SingleChoice ||questionType == QuestionTypes.Rating)
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
