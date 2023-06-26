using SurveyMonkey.DataTransferObject.Response;
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

        // TODO : 01 anket oluşturmayı yap.
    }
}
