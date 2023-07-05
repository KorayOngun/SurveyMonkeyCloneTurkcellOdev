using SurveyMonkey.DataTransferObject.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyMonkey.Business.IServices
{
    public interface ISurveyReportService
    {
        Task<SurveyReportResponse> GetReportAsync(int id, string userMail);
        Task<IEnumerable<QuestionLineAnswerReportResponse>> GetLineAnswerReport(int id, string email);

    }
}
