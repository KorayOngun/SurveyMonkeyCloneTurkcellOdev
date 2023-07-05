using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using SurveyMonkey.Business.IServices;
using SurveyMonkey.DataTransferObject.Request;
using System.Security.Claims;

namespace SurveyMonkey.MVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginRequest user)
        {
            if (ModelState.IsValid)
            {
                if (await _userService.Login(user))
                {
                    Claim[] claims = new Claim[]
                    {
                        new(ClaimTypes.Email,user.Email),
                    };
                    
                    ClaimsIdentity identity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
                    ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                  
                    await HttpContext.SignInAsync(principal);
                    return Redirect("/home/index");
                }
                return View(nameof(Login));  
            }

            return View(nameof(Login));

        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/home/index");
        }
    }
}
