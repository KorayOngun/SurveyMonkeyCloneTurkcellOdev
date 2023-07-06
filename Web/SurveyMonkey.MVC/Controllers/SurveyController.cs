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
        private readonly ISurveyReportService _surveyReportService;

        public SurveyController(ISurveyService surveyService, ISurveyReportService surveyReportService)
        {
            _surveyService = surveyService;
            _surveyReportService = surveyReportService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            var data = await _surveyService.GetSurveyByIdAForResponseAsync(id);
            if (data == default)
            {
                return GetErrorPage("id yanlış veya anketin süresi bitmiş :(");
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
                            createSingleChoice(formItem, questionId, singleChoiceForAnswerRequests);
                            break;
                        case QuestionTypes.MultiChoice:
                            createMultiChoice(formItem, questionId, multiChoiceForAnswerRequests);
                            break;
                        case QuestionTypes.SingleLine or QuestionTypes.MultiLine:
                            createLineResponse(formItem, questionId, lineResponseForAnswerRequests);
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
        public async Task<IActionResult> LineAnswersReport(int id)
        {
            var mail = GetMail();
            var data = await _surveyReportService.GetLineAnswerReport(id, mail);
            if (data == default)
            {
                return GetErrorPage("anket id'si yanlış veya anket sahibi değilsiniz :(");
            }
            return Json(data);
        }




        [Authorize]
        public async Task<IActionResult> GetSurveyList()
        {
            var mail = GetMail();
            var data = await _surveyService.GetSurveysAsync(mail);
            return View(data);
            
        }

        [Authorize]
        [ResponseCache(Duration = 10, Location = ResponseCacheLocation.Any ,NoStore =false, VaryByQueryKeys =new[] {"id"} )]
        public async Task<IActionResult> Report(int id)
        {
            
            var mail =GetMail();
            var data = await _surveyReportService.GetReportAsync(id,mail);
            if (data == default)
            {
                return GetErrorPage("anket id'si yanlış veya anket sahibi değilsiniz :(");
            }
            return View(data);
        }

        private string GetMail()
        {
            var mail = User.Claims.First(c => c.Type == ClaimTypes.Email).Value;
            return mail;
        }
        private RedirectToActionResult GetErrorPage(string message)
        {
            return RedirectToAction("Error", "Home", new { message });
        }
        private void createLineResponse(KeyValuePair<string, StringValues> formItem, int questionId, List<LineResponseForAnswerRequest> list)
        {
            var item = new LineResponseForAnswerRequest
            {
                QuestionId = questionId,
                Text = formItem.Value,
            };
            list.Add(item);
        }

        private void createMultiChoice(KeyValuePair<string, StringValues> formItem, int questionId, List<MultiChoiceForAnswerRequest> list)
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

        private void createSingleChoice(KeyValuePair<string, StringValues> formItem, int questionId, IList<SingleChoiceForAnswerRequest> list)
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
