using SurveyMonkey.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyMonkey.DataAccess.IRepos
{
    public interface ISurveyRepo : IRepo<Survey>,IDeletable<Survey>
    {
        Task<Survey> GetByIdForReportAsync(int id,string userMail);
        Task<int> GetCountChoice(int choiceId, int questionType);
        Task<int> GetCountParticipant(int id);
    }
}
