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
        Task<SurveyResponse> GetSurveyByIdAsync(int id);

        Task<int> CreateSurveyAsync(SurveyCreateRequest survey);
        Task<SurveyReportResponse> GetReportAsync(int id,string userMail);
    }
}
