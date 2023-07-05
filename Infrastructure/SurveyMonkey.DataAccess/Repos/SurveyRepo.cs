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
            var item = await _context.Surveys.Where(item => item.Id == id)
                                             .AsNoTracking()
                                             .Include(s => s.Questions)
                                             .ThenInclude(q => q.Choices)
                                             .Include(s => s.Questions)
                                             .ThenInclude(q => q.QuestionType)
                                             .Include(s => s.User)
                                             .FirstOrDefaultAsync();
           
            return item;
        }

        public async Task<Survey> GetByIdForReportAsync(int id, string userMail)
        {
            var item = await _context.Surveys.AsNoTracking()
                                             .Include(s => s.User)
                                             .Where(item => item.Id == id && item.User.Email == userMail)
                                             .Include(s => s.Questions)
                                             .ThenInclude(q => q.Choices)
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
            if (questionType==2)
            {
                count = await _context.MultiChoiceAnswers.CountAsync(s => s.ChoiceId == choiceId);
            }
            else
            {
                count = await _context.SingleChoiceAnswers.CountAsync(s => s.ChoiceId == choiceId);
            }
            return count;
        }
        public async Task<IEnumerable<LineAnswer>> LineAnswersForReport(int id)
        {
            IEnumerable<LineAnswer> items = await _context.LineAnswers.Where(l=>l.QuestionId == id).ToListAsync();
            return items;
        }

        public async Task<Survey> GetSurveyForAddAnswerControl(int id)
        {
            var item = await _context.Surveys.AsNoTracking().Where(s => s.Id == id).Include(s => s.Questions).ThenInclude(q => q.Choices).FirstOrDefaultAsync();
            return item;
        }

        public async Task AddAnswerToSurvey(Answer answer)
        {
            var item = await _context.Surveys.Where(s=>s.Id == answer.SurveyId).FirstOrDefaultAsync();
            item.Answers.Add(answer);  
            await _context.SaveChangesAsync();
        }

        public async Task<bool> isExist(Expression<Func<Survey, bool>> predicate)
        {
            if (await _context.Surveys.AnyAsync(predicate))
            {
                return true;
            }
            return false;
        }
    }
}
