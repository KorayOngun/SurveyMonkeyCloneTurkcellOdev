using SurveyMonkey.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SurveyMonkey.DataAccess.IRepos
{
    public interface ISurveyRepo : IRepo<Survey>,IDeletable<Survey>
    {
        Task AddAnswerToSurvey(Answer answer);
        Task<Survey> GetByIdForReportAsync(int id, string userMail);
        
        Task<Survey> GetSurveyForAddAnswerControl(int id);
        Task<IEnumerable<Survey>> GetSurveysForList(Expression<Func<Survey, bool>> filter);
    }
}
