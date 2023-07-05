using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyMonkey.DataAccess.IRepos
{
    public interface IMultiChoiceRepo
    {
        Task<int> GetCountChoice(int choiceId);

    }
}
