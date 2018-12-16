using System.Linq;
using AutoMapper;
using Data.DTOs;
using Data.Entities;

namespace Data
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Contact, ContactDTO>().ReverseMap();
            CreateMap<ContactType, ContactTypeDTO>().ReverseMap();
            CreateMap<District, DistrictDTO>().ReverseMap();
            CreateMap<Phone, PhoneDTO>().ReverseMap();
            CreateMap<Specialization, SpecializationDTO>().ReverseMap();
            CreateMap<Subject, SubjectDTO>().ReverseMap();

            CreateMap<Tutor, TutorDTO>()
                .ForMember(
                    dest => dest.Specializations,
                    opts => opts.MapFrom(
                        src => src.TutorSpecializations.Select(y => y.Specialization).ToList()))
                .ForMember(
                    dest => dest.Subjects,
                    opts => opts.MapFrom(
                        src => src.TutorSubjects.Select(y => y.Subject).ToList()));

            CreateMap<TutorDTO, Tutor>()
                .ForMember(
                    dest => dest.TutorSpecializations,
                    opts => opts.MapFrom(
                        src => src.Specializations.Select(y => new TutorSpecialization
                        {
                            TutorId = src.Id,
                            SpecializationId = y.Id
                        }).ToList()))
                .ForMember(
                    dest => dest.TutorSubjects,
                    opts => opts.MapFrom(
                        src => src.Subjects.Select(y => new TutorSubject
                        {
                            TutorId = src.Id,
                            SubjectId = y.Id
                        }).ToList()));
        }
    }
}
