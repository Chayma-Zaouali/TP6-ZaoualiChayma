using AutoMapper;
using SchoolAPI.Models;
using SchoolAPI.DTOs;

namespace SchoolAPI.Mapping
{
    public class SchoolProfile : Profile
    {
        public SchoolProfile()
        {
            CreateMap<School, SchoolDto>();
            CreateMap<SchoolDto, School>()
            .ForMember(dest => dest.Director, opt => opt.MapFrom(src => "")); 


        }
    }
}
