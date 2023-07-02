using SurveyMonkey.Business.IServices;
using SurveyMonkey.Business.MapperProfile;
using SurveyMonkey.Business.Services;
using SurveyMonkey.DataAccess.Context;
using SurveyMonkey.DataAccess.IRepos;
using SurveyMonkey.DataAccess.Repos;

using SurveyMonkey.Entities;
using SurveyMonkey.WebApi.Middlewares;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

using var logger =  LoggerFactory.Create(configure => configure.AddSimpleConsole());
logger.CreateLogger<ResponseTimerMiddleware>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddScoped<ISurveyRepo,SurveyRepo>();
builder.Services.AddScoped<ISurveyService,SurveyService>();
builder.Services.AddDbContext<SurveyMonkeyDbContext>();


builder.Services.AddAutoMapper(typeof(MapProfileDto));
builder.Services.AddAutoMapper(typeof(MapProfileVirtualDto));


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ResponseTimerMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
