using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurveyMonkey.Business.IServices;
using SurveyMonkey.DataTransferObject.Request;
using SurveyMonkey.DataTransferObject.Response;
using SurveyMonkey.Entities;

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
        public async Task<SurveyReportResponse> GetReportData(int id)
        {
            var item = await _surveyService.GetReportAsync(id);
            return item;
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Create(SurveyCreateRequest survey)
        {
           await  _surveyService.CreateSurveyAsync(survey);
            return Ok(new {id = survey.Id});
        }
    }
}
