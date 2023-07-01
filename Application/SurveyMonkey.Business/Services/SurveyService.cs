using AutoMapper;
using SurveyMonkey.Business.Extensions;
using SurveyMonkey.Business.IServices;
using SurveyMonkey.DataAccess.IRepos;
using SurveyMonkey.DataTransferObject.Request;
using SurveyMonkey.DataTransferObject.Response;
using SurveyMonkey.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyMonkey.Business.Services
{
    public class SurveyService : ISurveyService
    {
        private readonly ISurveyRepo _repo;
        private readonly IMapper _mapper;

        public SurveyService(ISurveyRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task CreateSurveyAsync(SurveyCreateRequest survey)
        {
            var item = _mapper.Map<Survey>(survey);
            await _repo.CreateAsync(item);
        }

        public async Task<SurveyReportResponse> GetReportAsync(int id)
        {
            var item = await _repo.GetByIdForReportAsync(id);

            SurveyReportResponse report = await generateReport(item);


            return report;
        }

        private async Task<SurveyReportResponse> generateReport(Survey item)
        {

            var survey = new SurveyReportResponse
            {
                SurveyId = item.Id,
                SurveyName = item.Name,
                Questions = await getQuestions(item)
            };
            return survey;
        }

       

        private async Task<IList<SurveyReportQuestionView>> getQuestions(Survey item)
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
                        Count = getCount(choices.Id,item.Answers)
                    };
                    questionView.Choices.Add(choicesView);
                }
                questionList.Add(questionView);
            }
            return questionList;
        }

        private int getCount(int id, ICollection<Answer> answers)
        {
            var answer = answers.First(); 
                var count = Math.Max(answer.SingleChoiceAnswer.Count(c=>c.ChoiceId==id),answer.MultiChoiceAnswer.Count(c=>c.ChoiceId==id));
            return count;
        }

        public async Task<SurveyResponse> GetSurveyByIdAsync(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            return item.ConvertToDto<SurveyResponse>(_mapper);
        }
    }
}
