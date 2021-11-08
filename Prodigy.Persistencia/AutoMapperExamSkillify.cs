using AutoMapper;
using Prodigy.Modelo.examSkillify;
using Prodigy.Utils.DTO;
using System;

namespace Prodigy.Persistencia
{
    public class AutoMapperExamSkillify : Profile
    {
        public AutoMapperExamSkillify()
        {
            CreateMap<Users, DTOUsers>().ReverseMap();
        }

        public static void Configuration() { }

    }
}