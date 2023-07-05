using Microsoft.EntityFrameworkCore;
using SurveyMonkey.Business.IServices;
using SurveyMonkey.Business.MapperProfile;
using SurveyMonkey.Business.Services;
using SurveyMonkey.DataAccess.Context;
using SurveyMonkey.DataAccess.IRepos;
using SurveyMonkey.DataAccess.Repos;

namespace SurveyMonkey.MVC.Extension
{
    public static class IoCExtension
    {
        public static void AddInjection(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();


            builder.Services.AddScoped<ISurveyRepo, SurveyRepo>();
            builder.Services.AddScoped<ISingleChoiceRepo, SingleChoiceRepo>();
            builder.Services.AddScoped<IMultiChoiceRepo, MultiChoiceRepo>();
            builder.Services.AddScoped<IAnswerRepo, AnswerRepo>();
            builder.Services.AddScoped<ILineAnswerRepo, LineAnswerRepo>();
            builder.Services.AddScoped<ISurveyService, SurveyService>();

            builder.Services.AddScoped<IUserRepo, UserRepo>();
            builder.Services.AddScoped<IUserService, UserService>();

            var connectionString = builder.Configuration.GetConnectionString("SqlCon");
            builder.Services.AddDbContext<SurveyMonkeyDbContext>(opt => opt.UseSqlServer(connectionString));

            builder.Services.AddAutoMapper(typeof(MapProfileDto));
            builder.Services.AddAutoMapper(typeof(MapProfileVirtualDto));



            
        }
    }
}
