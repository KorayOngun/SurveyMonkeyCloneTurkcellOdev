using Microsoft.EntityFrameworkCore;
using SurveyMonkey.DataAccess.Context;
using SurveyMonkey.DataAccess.IRepos;
using SurveyMonkey.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyMonkey.DataAccess.Repos
{
    public class SurveyRepo : ISurveyRepo
    {
        private readonly SurveyMonkeyDbContext _context;

        public SurveyRepo(SurveyMonkeyDbContext context)
        {
            _context = context;
        }

        public void Create(Survey entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
        }

        public async Task CreateAsync(Survey entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _context.Surveys.FindAsync(id);
            _context.Surveys.Remove(item);
            await _context.SaveChangesAsync();
        }

        public Survey GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Survey> GetByIdAsync(int id)
        {
            var item = await _context.Surveys.Where(item => item.Id == id).AsNoTracking()
                                        .Include(s => s.Questions).ThenInclude(q => q.Choices)
                                        .Include(s => s.Questions).ThenInclude(q => q.QuestionType)
                                        .Include(s=>s.User)
                                        .FirstOrDefaultAsync();
           
            return item;
        }
    }
}
