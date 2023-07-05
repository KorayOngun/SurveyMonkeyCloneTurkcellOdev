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
    public class LineAnswerRepo : ILineAnswerRepo
    {
        private readonly SurveyMonkeyDbContext _context;

        public LineAnswerRepo(SurveyMonkeyDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<LineAnswer>> LineAnswersForReport(int id)
        {
            IEnumerable<LineAnswer> items = await _context.LineAnswers.Where(l => l.QuestionId == id).ToListAsync();
            return items;
        }
    }
}
