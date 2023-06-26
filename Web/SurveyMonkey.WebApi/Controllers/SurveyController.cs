using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurveyMonkey.Business.IServices;
using SurveyMonkey.DataTransferObject.Response;

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
    }
}
