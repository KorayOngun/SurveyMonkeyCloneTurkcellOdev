using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SurveyMonkey.Business.IServices;
using SurveyMonkey.DataTransferObject.Request;
using SurveyMonkey.DataTransferObject.Response;
using SurveyMonkey.Entities;
using SurveyMonkey.WebApi.Extension;
using System.Buffers.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SurveyMonkey.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyController : ControllerBase
    {

        private readonly ISurveyService _surveyService;
        private readonly ISurveyReportService _surveyReportService;
        private readonly IConfiguration _configuration;
        public SurveyController(ISurveyService surveyService, ISurveyReportService surveyReportService, IConfiguration configuration)
        {
            _surveyService = surveyService;
            _surveyReportService = surveyReportService;
            _configuration = configuration;
        }

        [HttpGet("[action]")]
        public async Task<SurveyResponse> GetSurvey(int id) 
        {
            var item = await _surveyService.GetSurveyByIdAForResponseAsync(id);

            return item;
        }


        [HttpGet("[action]")]
        [Authorize]
        public async Task<SurveyReportResponse> GetReportData(int id)
        {
            var mail = HttpContext.Request.Headers.GetAuthorizationValues(JwtRegisteredClaimNames.Email);
            
            var item = await _surveyReportService.GetReportAsync(id,mail);
            return item;
        }
      
        [HttpPost("[action]")]
        [Authorize]
        public async Task<IActionResult> Create(SurveyCreateRequest survey)
        {
            var id = await  _surveyService.CreateSurveyAsync(survey);
            var userId = Convert.ToInt32(HttpContext.Request.Headers.GetAuthorizationValues(JwtRegisteredClaimNames.UniqueName));
            survey.UserId = userId;
            var MvcPath = _configuration.GetValue<string>("MvcUrl");
            string path = MvcPath + id.ToString();
            return Created(path, survey);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddAnswer(AnswerRequest answer)
        {
            await _surveyService.AddAnswer(answer);
            return Ok();
        }

    }
}
