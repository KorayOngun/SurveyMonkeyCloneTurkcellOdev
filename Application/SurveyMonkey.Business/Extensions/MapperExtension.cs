using AutoMapper;
using SurveyMonkey.DataTransferObject;
using SurveyMonkey.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyMonkey.Business.Extensions
{
    public static class MapperExtension
    {
        public static T ConvertToDto<T>(this IEntity entity, IMapper mapper) where T : class, IDto, new()
        {
            return mapper.Map<T>(entity);
        }

    }
}
