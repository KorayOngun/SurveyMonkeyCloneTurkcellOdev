using Microsoft.EntityFrameworkCore;
using SurveyMonkey.DataAccess.Context;
using SurveyMonkey.DataAccess.IRepos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyMonkey.DataAccess.Repos
{
    public class AnswerRepo : IAnswerRepo
    {
        private readonly SurveyMonkeyDbContext _context;

        public AnswerRepo(SurveyMonkeyDbContext context)
        {
            _context = context;
        }
        public async Task<int> GetCountParticipant(int id)
        {
            var count = await _context.Answers.CountAsync(a => a.SurveyId == id);
            return count;
        }
    }
}
