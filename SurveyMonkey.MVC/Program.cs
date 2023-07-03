using SurveyMonkey.Business.IServices;
using SurveyMonkey.Business.Services;
using SurveyMonkey.Business.MapperProfile;

using SurveyMonkey.DataAccess.Context;
using SurveyMonkey.DataAccess.IRepos;
using SurveyMonkey.DataAccess.Repos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<ISurveyRepo, SurveyRepo>();
builder.Services.AddScoped<ISurveyService, SurveyService>();
builder.Services.AddDbContext<SurveyMonkeyDbContext>();



builder.Services.AddAutoMapper(typeof(MapProfileDto));
builder.Services.AddAutoMapper(typeof(MapProfileVirtualDto));

builder.Services.AddResponseCaching();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseResponseCaching();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=survey}/{action=report}/{id=2}");

app.Run();
