using AutoMapper;
using Vezeeta.Core.Contracts.DoctorDtos;
using Vezeeta.Core.Enums;
using Vezeeta.Core.Models;
using Vezeeta.Core.Models.Identity;

namespace Vezeeta.Api.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateDoctorDto, ApplicationUser>();
        CreateMap<CreateDoctorDto, Doctor>().ForMember(e => e.ApplicationUser, act => act.MapFrom(src => src));

        CreateMap<Doctor, GetDoctorDto>().ForMember(e => e.FullName, act => act.MapFrom(src => src.ApplicationUser.FirstName + ' ' + src.ApplicationUser.LastName))
                                         .ForMember(e => e.Specialize, act => act.MapFrom(src => src.Specialization.Name))
                                         .ForMember(e => e.Email, act => act.MapFrom(src => src.ApplicationUser.Email))
                                         .ForMember(e => e.PhotoPath, act => act.MapFrom(src => src.ApplicationUser.PhotoPath))
                                         .ForMember(e => e.Phone, act => act.MapFrom(src => src.ApplicationUser.PhoneNumber))
                                         .ForMember(e => e.Gender, act => act.MapFrom(src => src.ApplicationUser.Gender));

        CreateMap<Doctor, GetIdDoctorDto>().ForMember(e => e.FullName, act => act.MapFrom(src => src.ApplicationUser.FirstName + ' ' + src.ApplicationUser.LastName))
                                         .ForMember(e => e.Specialize, act => act.MapFrom(src => src.Specialization.Name))
                                         .ForMember(e => e.Email, act => act.MapFrom(src => src.ApplicationUser.Email))
                                         .ForMember(e => e.PhotoPath, act => act.MapFrom(src => src.ApplicationUser.PhotoPath))
                                         .ForMember(e => e.Phone, act => act.MapFrom(src => src.ApplicationUser.PhoneNumber))
                                         .ForMember(e => e.DateOfBirth, act => act.MapFrom(src => src.ApplicationUser.DateOfBirth))
                                         .ForMember(e => e.Gender, act => act.MapFrom(src => src.ApplicationUser.Gender));

    }
}
