using SurveyMonkey.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SurveyMonkey.DataAccess.IRepos
{
    public interface IRepo<T> where T : class,IEntity, new()
    {
        Task<T> GetByIdAsync(int id);
        T GetById(int id);

        Task CreateAsync(T entity);
        void Create(T entity);
        Task<bool> isExist(Expression<Func<T, bool>> predicate);
        
    }
}
