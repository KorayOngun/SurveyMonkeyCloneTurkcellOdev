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

    }
}
