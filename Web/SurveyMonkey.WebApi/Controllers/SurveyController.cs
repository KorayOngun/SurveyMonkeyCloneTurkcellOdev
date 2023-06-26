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

        [HttpGet]
        public async Task<SurveyResponse> Get(int id) 
        {
            var item = await _surveyService.GetSurveyByIdAsync(id);
            return item;
        }
        [HttpPost]
        public async Task<IActionResult> Create(SurveyCreateRequest survey)
        {
           await  _surveyService.CreateSurveyAsync(survey);
            return Ok(new {id = survey.Id});
        }
    }
}
