using AutoMapper;
using Vezeeta.Core.Contracts.DoctorDtos;
using Vezeeta.Core.Enums;
using Vezeeta.Core.Models;

namespace Vezeeta.Api.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateDoctorDto, User>().ForMember(e => e.UserType, act => act.MapFrom(src => UserType.Doctor));
        CreateMap<CreateDoctorDto, Doctor>().ForMember(e => e.User, act => act.MapFrom(src => src));

        CreateMap<Doctor, GetDoctorDto>().ForMember(e => e.FullName, act => act.MapFrom(src => src.User.FirstName + ' ' + src.User.LastName))
                                         .ForMember(e => e.Specialize, act => act.MapFrom(src => src.Specialization.Name))
                                         .ForMember(e => e.Email, act => act.MapFrom(src => src.User.Email))
                                         .ForMember(e => e.PhotoPath, act => act.MapFrom(src => src.User.PhotoPath))
                                         .ForMember(e => e.Phone, act => act.MapFrom(src => src.User.Phone))
                                         .ForMember(e => e.Gender, act => act.MapFrom(src => src.User.Gender));
        CreateMap<Doctor, GetIdDoctorDto>().ForMember(e => e.FullName, act => act.MapFrom(src => src.User.FirstName + ' ' + src.User.LastName))
                                         .ForMember(e => e.Specialize, act => act.MapFrom(src => src.Specialization.Name))
                                         .ForMember(e => e.Email, act => act.MapFrom(src => src.User.Email))
                                         .ForMember(e => e.PhotoPath, act => act.MapFrom(src => src.User.PhotoPath))
                                         .ForMember(e => e.Phone, act => act.MapFrom(src => src.User.Phone))
                                         .ForMember(e => e.DateOfBirth, act => act.MapFrom(src => src.User.DateOfBirth))
                                         .ForMember(e => e.Gender, act => act.MapFrom(src => src.User.Gender));

    }
}
