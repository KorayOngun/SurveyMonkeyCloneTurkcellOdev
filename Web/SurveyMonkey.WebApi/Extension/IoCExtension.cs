using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SurveyMonkey.Business.IServices;
using SurveyMonkey.Business.MapperProfile;
using SurveyMonkey.Business.Services;
using SurveyMonkey.DataAccess.Context;
using SurveyMonkey.DataAccess.IRepos;
using SurveyMonkey.DataAccess.Repos;
using System.Text;

namespace SurveyMonkey.WebApi.Extension
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
            builder.Services.AddScoped<ISurveyReportService, SurveyReportService>();


            var connectionString = builder.Configuration.GetConnectionString("SqlCon");
            builder.Services.AddDbContext<SurveyMonkeyDbContext>(opt => opt.UseSqlServer(connectionString));

            builder.Services.AddAutoMapper(typeof(MapProfileDto));
            builder.Services.AddAutoMapper(typeof(MapProfileVirtualDto));

            var JwtKey = builder.Configuration.GetValue<string>("JwtKey");
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                             .AddJwtBearer(opt =>
                             {
                                 opt.TokenValidationParameters = new TokenValidationParameters
                                 {
                                     ValidateIssuer = true,
                                     ValidateAudience = true,
                                     ValidIssuer = "server",
                                     ValidAudience = "client",
                                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtKey)),
                                 };
                             });



        }
    }
}
