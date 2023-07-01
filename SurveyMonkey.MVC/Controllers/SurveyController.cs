﻿using Microsoft.AspNetCore.Mvc;
using SurveyMonkey.Business.IServices;

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
    }
}
