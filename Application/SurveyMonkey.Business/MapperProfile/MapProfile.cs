using AutoMapper;
using AutoMapper.Features;
using SurveyMonkey.DataTransferObject.Response;
using SurveyMonkey.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyMonkey.Business.MapperProfile
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {

            CreateMap<Survey, SurveyResponse>().ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Name));
        }
    }
}
