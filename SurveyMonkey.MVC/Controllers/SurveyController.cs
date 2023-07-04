using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurveyMonkey.Business.IServices;
using System.Security.Claims;

namespace SurveyMonkey.MVC.Controllers
{
    public class SurveyController : Controller
    {
        private readonly ISurveyService _surveyService;

        public SurveyController(ISurveyService surveyService)
        {
            _surveyService = surveyService;
        }

        public async Task<IActionResult> Index(int id)
        {
            var data = await _surveyService.GetSurveyByIdAsync(id);
            return View(data);
        }


        [Authorize]
        [ResponseCache(Duration = 10, Location = ResponseCacheLocation.Any ,NoStore =false, VaryByQueryKeys =new[] {"id"} )]
        public async Task<IActionResult> Report(int id)
        {
            
            var mail = User.Claims.First(c => c.Type == ClaimTypes.Email).Value;
            var data = await _surveyService.GetReportAsync(id,mail);
            return View(data);
        }
    }
}
