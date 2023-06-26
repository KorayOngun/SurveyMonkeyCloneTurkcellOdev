using AutoMapper;
using SurveyMonkey.Business.IServices;
using SurveyMonkey.DataAccess.IRepos;
using SurveyMonkey.DataTransferObject.Request;
using SurveyMonkey.DataTransferObject.Response;
using SurveyMonkey.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyMonkey.Business.Services
{
    public class SurveyService : ISurveyService
    {
        private readonly ISurveyRepo _repo;
        private readonly IMapper _mapper;

        public SurveyService(ISurveyRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task CreateSurveyAsync(SurveyCreateRequest survey)
        {
            var item = _mapper.Map<Survey>(survey);
            await _repo.CreateAsync(item);
        }

        public async Task<SurveyResponse> GetSurveyByIdAsync(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            return _mapper.Map<SurveyResponse>(item);   
        }
    }
}
