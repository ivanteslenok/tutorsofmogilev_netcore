using AutoMapper;
using Data.DTOs;
using System.Linq;

namespace TutorsOfMogilev_NetCore.Models
{
    public class VMMapperProfile : Profile
    {
        public VMMapperProfile()
        {
            CreateMap<ResumeVM, TutorDTO>();

            CreateMap<TutorDTO, TutorAdvancedVM>()
                .ForMember(
                    dest => dest.District,
                    opts => opts.MapFrom(src => src.District.Name))
                .ForMember(
                    dest => dest.Specializations,
                    opts => opts.MapFrom(src => src.Specializations.Select(x => x.Name)))
                .ForMember(
                    dest => dest.Subjects,
                    opts => opts.MapFrom(src => src.Subjects.Select(x => x.Name)));
        }
    }
}
