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
    public class SingleChoiceRepo : ISingleChoiceRepo
    {

        private readonly SurveyMonkeyDbContext _context;

        public SingleChoiceRepo(SurveyMonkeyDbContext context)
        {
            _context = context;
        }
        public async Task<int> GetCountChoice(int choiceId)
        {
           int count = await _context.SingleChoiceAnswers.CountAsync(s => s.ChoiceId == choiceId);
           return count;
        }
    }
}
