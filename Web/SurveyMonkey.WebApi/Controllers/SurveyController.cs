using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurveyMonkey.Business.IServices;
using SurveyMonkey.DataTransferObject.Request;
using SurveyMonkey.DataTransferObject.Response;
using SurveyMonkey.Entities;
using System.IdentityModel.Tokens.Jwt;

namespace SurveyMonkey.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyController : ControllerBase
    {

        private readonly ISurveyService _surveyService;

        public SurveyController(ISurveyService surveyService)
        {
            _surveyService = surveyService;
        }


        [HttpGet("[action]")]
        public async Task<SurveyResponse> GetSurvey(int id) 
        {
            var item = await _surveyService.GetSurveyByIdAsync(id);
            return item;
        }


        [HttpGet("[action]")]
        [Authorize]
        public async Task<SurveyReportResponse> GetReportData(int id,string mail)
        {
            var item = await _surveyService.GetReportAsync(id,mail);
            return item;
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<IActionResult> Create(SurveyCreateRequest survey)
        {
            var id = await  _surveyService.CreateSurveyAsync(survey);
            string path = "https://localhost:7104/survey/index?id=" + id.ToString();
            return Created(path, survey);
        }

    }
}
