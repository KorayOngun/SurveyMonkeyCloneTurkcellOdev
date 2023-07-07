using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SurveyMonkey.Business.IServices;
using SurveyMonkey.Business.MapperProfile;
using SurveyMonkey.Business.Services;
using SurveyMonkey.DataAccess.Context;
using SurveyMonkey.DataAccess.IRepos;
using SurveyMonkey.DataAccess.Repos;

using SurveyMonkey.Entities;
using SurveyMonkey.WebApi.Extension;
using SurveyMonkey.WebApi.Middlewares;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

using var logger =  LoggerFactory.Create(configure => configure.AddSimpleConsole());
logger.CreateLogger<ResponseTimerMiddleware>();

builder.AddInjection();
builder.InitConfig();



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using var scope = app.Services.CreateScope();
var service = scope.ServiceProvider;
var context = service.GetRequiredService<SurveyMonkeyDbContext>();
context.Database.EnsureCreated();

app.UseHttpsRedirection();

app.UseMiddleware<ResponseTimerMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
