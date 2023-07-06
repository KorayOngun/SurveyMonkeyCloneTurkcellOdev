using AutoMapper;
using AutoMapper.Features;
using SurveyMonkey.DataTransferObject.Request;
using SurveyMonkey.DataTransferObject.Response;
using SurveyMonkey.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyMonkey.Business.MapperProfile
{
    public class MapProfileDto : Profile
    {
        public MapProfileDto()
        {

            CreateMap<Survey, SurveyResponse>().ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Name));
            CreateMap<Survey, SurveyListResponse>();
            CreateMap<Survey, SurveyReportResponse>();

            CreateMap<SurveyCreateRequest, Survey>();


            CreateMap<UserLoginRequest, User>();
            CreateMap<UserCreateRequest, User>();
            
            CreateMap<AnswerRequest, Answer>();

        }
    }
}
