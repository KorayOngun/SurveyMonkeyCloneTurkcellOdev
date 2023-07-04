using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SurveyMonkey.Business.IServices;
using SurveyMonkey.DataTransferObject.Request;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SurveyMonkey.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly IConfiguration _configuration;

        public UserController(IUserService service, IConfiguration configuration)
        {
            _service = service;
            _configuration = configuration;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(UserLoginRequest user)
        {
            if (ModelState.IsValid)
            {
                if (await _service.Login(user))
                {
                    Claim[] claims = new Claim[]
                    {
                        new(JwtRegisteredClaimNames.Email,user.Email),
                    };

                    var jwtKey = _configuration.GetValue<string>("JwtKey");

                    var keyValue = Encoding.UTF8.GetBytes(jwtKey);

                    var key = new SymmetricSecurityKey(keyValue);

                    var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(
                             issuer: "server",
                            audience: "client",
                            claims: claims,
                            signingCredentials: credential,
                            notBefore: DateTime.Now,
                            expires: DateTime.Now.AddDays(1)
                        );
                    return Ok(new {token = new JwtSecurityTokenHandler().WriteToken(token)});
                }
                return NotFound();
            }
            return BadRequest();
        }
    }
}
