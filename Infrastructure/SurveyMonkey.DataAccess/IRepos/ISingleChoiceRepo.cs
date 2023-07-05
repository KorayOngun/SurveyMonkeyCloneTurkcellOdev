using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyMonkey.DataAccess.IRepos
{
    public interface ISingleChoiceRepo 
    {
        Task<int> GetCountChoice(int choiceId);
    }
}
