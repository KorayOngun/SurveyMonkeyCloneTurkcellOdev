using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using SurveyMonkey.Business.Helper;
using SurveyMonkey.Business.IServices;
using SurveyMonkey.DataTransferObject.Request;
using System.Collections.Generic;
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

        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            var data = await _surveyService.GetSurveyByIdAsync(id);
            if (data == default)
            {
                return View("Error","Home");
            }
            return View(data);
        }

        [HttpPost]
        public  async Task<IActionResult> Index(IFormCollection form,int id)
        {
            AnswerRequest answer = new AnswerRequest();
            List<SingleChoiceForAnswerRequest> singleChoiceForAnswerRequests = new List<SingleChoiceForAnswerRequest>();
            List<MultiChoiceForAnswerRequest> multiChoiceForAnswerRequests = new List<MultiChoiceForAnswerRequest>();
            List<LineResponseForAnswerRequest> lineResponseForAnswerRequests = new List<LineResponseForAnswerRequest>();
            foreach (var formItem in form)
            {
                
                var key = formItem.Key;
                if (!key.StartsWith("__"))
                {
                    var questionId = Convert.ToInt32(key.Split(",").First());
                    var questionTypeId = Convert.ToInt32(key.Split(",").Last());
                    switch (questionTypeId)
                    {
                        case QuestionTypes.SingleChoice or QuestionTypes.Rating:
                            createSingleChoice(key, formItem, questionId, singleChoiceForAnswerRequests);
                            break;
                        case QuestionTypes.MultiChoice:
                            createMultiChoice(key, formItem, questionId, multiChoiceForAnswerRequests);
                            break;
                        case QuestionTypes.SingleLine or QuestionTypes.MultiLine:
                            createLineResponse(key, formItem, questionId, lineResponseForAnswerRequests);
                            break;
                    }
                }
                continue;
            }
            answer.SingleChoiceAnswer = singleChoiceForAnswerRequests;
            answer.MultiChoiceAnswer = multiChoiceForAnswerRequests;
            answer.lineAnswers = lineResponseForAnswerRequests;
            answer.SurveyId = id;
            await _surveyService.AddAnswer(answer);
            return RedirectToAction("Index", "Home");
            
        }


        [HttpGet]
        [ResponseCache(Duration = 10, Location = ResponseCacheLocation.Any, NoStore = false, VaryByQueryKeys = new[] { "id" })]
        public async Task<IActionResult> LineAnswers(int id)
        {
            var mail = User.Claims.First(c => c.Type == ClaimTypes.Email).Value;
            var data = await _surveyService.GetLineAnswerReport(id, mail);
            if (data == default)
            {
                return View("Error", "Home");
            }
            
            return Json(data);
        }



        [Authorize]
        [ResponseCache(Duration = 10, Location = ResponseCacheLocation.Any ,NoStore =false, VaryByQueryKeys =new[] {"id"} )]
        public async Task<IActionResult> Report(int id)
        {
            
            var mail = User.Claims.First(c => c.Type == ClaimTypes.Email).Value;
            var data = await _surveyService.GetReportAsync(id,mail);
            if (data == default)
            {
                return View("Error", "Home");
            }
            return View(data);
        }

        private void createLineResponse(string key, KeyValuePair<string, StringValues> formItem, int questionId, List<LineResponseForAnswerRequest> list)
        {
            var item = new LineResponseForAnswerRequest
            {
                QuestionId = questionId,
                Text = formItem.Value,
            };
            list.Add(item);
        }

        private void createMultiChoice(string key, KeyValuePair<string, StringValues> formItem, int questionId, List<MultiChoiceForAnswerRequest> list)
        {
            foreach (var choice in formItem.Value)
            {
                MultiChoiceForAnswerRequest answer = new MultiChoiceForAnswerRequest
                {
                    QuestionId = questionId,
                    ChoiceId = Convert.ToInt32(choice)
                };
                list.Add(answer);
            }
        }

        private void createSingleChoice(string key, KeyValuePair<string, StringValues> formItem, int questionId, IList<SingleChoiceForAnswerRequest> list)
        {
            var item = new SingleChoiceForAnswerRequest
            {
                QuestionId = questionId,
                ChoiceId = Convert.ToInt32(formItem.Value),
            };
            list.Add(item);
        }
    }
}
