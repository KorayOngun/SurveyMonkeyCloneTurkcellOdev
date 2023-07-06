using Microsoft.EntityFrameworkCore;
using SurveyMonkey.DataAccess.Context;
using SurveyMonkey.DataAccess.IRepos;
using SurveyMonkey.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SurveyMonkey.DataAccess.Repos
{
    public class UserRepo : IUserRepo
    {
        private readonly SurveyMonkeyDbContext _context;

        public UserRepo(SurveyMonkeyDbContext context)
        {
            _context = context;
        }

        public void Create(User entity)
        {
            throw new NotImplementedException();
        }

        public async Task CreateAsync(User entity)
        {
            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public User GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> isExist(Expression<Func<User, bool>> predicate)
        {
            if (await _context.Users.AnyAsync(predicate))
            {
                return true;
            }
            return false;
        }

        public async Task<int> ValidateUser(User user)
        {
            var item = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email && u.Password == user.Password);
            if (item != null)
            {
                return item.Id;
            }
            return 0;
        }
    }
}
