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

        public SurveyController(ISurveyService surveyService, ISurveyReportService surveyReportService)
        {
            _surveyService = surveyService;
            _surveyReportService = surveyReportService;
        }

        [HttpGet("[action]")]
        public async Task<SurveyResponse> GetSurvey(int id) 
        {
            var item = await _surveyService.GetSurveyByIdAsync(id);
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
            string path = "https://localhost:7104/survey/index?id=" + id.ToString();
            return Created(path, survey);
        }

    }
}
