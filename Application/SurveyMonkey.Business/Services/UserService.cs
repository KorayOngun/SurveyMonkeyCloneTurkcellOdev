using AutoMapper;
using SurveyMonkey.Business.Extensions;
using SurveyMonkey.Business.IServices;
using SurveyMonkey.DataAccess.IRepos;
using SurveyMonkey.DataTransferObject.Request;
using SurveyMonkey.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyMonkey.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _repo;
        private readonly IMapper _mapper;

        public UserService(IUserRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<bool> CreateUser(UserCreateRequest user)
        {
            var userCondition = await _repo.isExist(u=>u.Email == user.Email); 
            if (!userCondition)
            {
                var item = user.ConvertToEntity<User>(_mapper);
                await _repo.CreateAsync(item);
                return true;
            }
            return false;
        }

        public async Task<int> Login(UserLoginRequest user)
        {
            var u = user.ConvertToEntity<User>(_mapper);
            var result = await _repo.ValidateUser(u);   
            return result;  
        }
    }
}
