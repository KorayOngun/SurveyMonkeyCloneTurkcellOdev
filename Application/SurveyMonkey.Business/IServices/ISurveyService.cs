using SurveyMonkey.DataTransferObject.Request;
using SurveyMonkey.DataTransferObject.Response;
using SurveyMonkey.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyMonkey.Business.IServices
{
    public interface ISurveyService
    {
        Task<SurveyResponse> GetSurveyByIdAForResponseAsync(int id);

        Task<int> CreateSurveyAsync(SurveyCreateRequest survey);
        
        Task AddAnswer(AnswerRequest answer);

        Task<IEnumerable<SurveyListResponse>> GetSurveysAsync(string userMail);
        Task DeleteSurvey(int surveyId,string mail);    
    }
}
