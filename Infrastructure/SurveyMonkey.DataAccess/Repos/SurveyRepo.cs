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
                                        .Include(s=>s.Questions).ThenInclude(q=>q.QuestionType)
                                        .Include(s => s.User)
                                        .FirstOrDefaultAsync();
           
            return item;
        }

        public async Task<Survey> GetByIdForReportAsync(int id)
        {
            var item = await _context.Surveys.Where(item => item.Id == id).AsNoTracking()
                                      .Include(s => s.Questions).ThenInclude(q => q.Choices)
                                      //.Include(s => s.Answers).ThenInclude(a => a.SingleChoiceAnswer)
                                      //.Include(s => s.Answers).ThenInclude(a => a.MultiChoiceAnswer)                                     
                                      .FirstOrDefaultAsync();

            return item;
        }
        public async Task<int> GetCountParticipant(int id)
        {
            var count = await _context.Answers.CountAsync(a => a.SurveyId == id);
            return count;
        }
        public async Task<int> GetCountChoice(int choiceId,int questionType)
        {
            int count = 0;  
            if (questionType==1)
            {
                count = await _context.SingleChoiceAnswers.CountAsync(s=>s.ChoiceId == choiceId);
            }
            else
            {
                count = await _context.MultiChoiceAnswers.CountAsync(s =>s.ChoiceId == choiceId);
            }
            return count;
        }
    }
}
