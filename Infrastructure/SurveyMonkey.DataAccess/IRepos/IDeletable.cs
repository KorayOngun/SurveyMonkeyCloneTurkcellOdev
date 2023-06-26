using SurveyMonkey.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyMonkey.DataAccess.IRepos
{
    public interface IDeletable<T> where T : class,IEntity,new()
    {
        Task DeleteAsync(int id);
        void Delete(int id);
    }
}
