﻿using AutoMapper;
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
    public class MapProfileVirtualDto : Profile
    {
        public MapProfileVirtualDto()
        {
            CreateMap<Question, QuestionView>().ForMember(dest => dest.QuestionTypeName, src => src.MapFrom(src => src.QuestionType.Name));

            CreateMap<QuestionForSurveyCreateRequest, Question>();
            CreateMap<ChoiceForSurveyCreateRequest, Choice>();
        }
    }
}
