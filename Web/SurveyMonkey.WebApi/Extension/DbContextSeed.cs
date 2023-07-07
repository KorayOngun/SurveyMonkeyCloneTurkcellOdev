using SurveyMonkey.DataAccess.Context;
using SurveyMonkey.Entities;

namespace SurveyMonkey.WebApi.Extension
{
    public static class DbContextSeed
    {
        public static void AddQuestionType(this SurveyMonkeyDbContext context)
        {
            if (!context.QuestionTypes.Any())
            {
                List<QuestionType> questionTypes = new List<QuestionType>
                {
                    new(){Name="single-choice"},
                    new(){Name="multi-choice"},
                    new(){Name="rating"},
                    new(){Name="single-line"},
                    new(){Name="multi-line"},
                };
                context.QuestionTypes.AddRange(questionTypes);
                context.SaveChanges();
            }

        }
    }
}
