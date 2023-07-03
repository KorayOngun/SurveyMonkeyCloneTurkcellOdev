using Microsoft.AspNetCore.Mvc;

namespace SurveyMonkey.MVC.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
