using Microsoft.AspNetCore.Mvc;
using SurveyMonkey.MVC.Models;
using System.Diagnostics;

namespace SurveyMonkey.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

       
        public IActionResult Error(string message)
        {
            return Json(new { message });
        }
    }
}