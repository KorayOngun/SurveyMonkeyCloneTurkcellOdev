using SurveyMonkey.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyMonkey.DataAccess.IRepos
{
    public interface IEditable<T> where T : class, IEntity, new()
    {
        Task UpdateAsync(T entity);
        void Update(T entity);
    }
}
