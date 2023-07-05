using SurveyMonkey.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyMonkey.DataAccess.IRepos
{
    public interface ILineAnswerRepo
    {
        Task<IEnumerable<LineAnswer>> LineAnswersForReport(int id);

    }
}
