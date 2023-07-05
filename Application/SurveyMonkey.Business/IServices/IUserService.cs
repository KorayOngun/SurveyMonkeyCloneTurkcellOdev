using SurveyMonkey.DataTransferObject.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyMonkey.Business.IServices
{
    public interface IUserService
    {
        Task<bool> Login(UserLoginRequest user);
        Task<bool> CreateUser(UserCreateRequest user);
    }
}
