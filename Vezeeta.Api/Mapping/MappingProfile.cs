using AutoMapper;
using Vezeeta.Core.Contracts;
using Vezeeta.Core.Enums;
using Vezeeta.Core.Models;

namespace Vezeeta.Api.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateDoctorDto, User>().ForMember(e => e.UserType, act => act.MapFrom(src => UserType.Doctor));
        CreateMap<CreateDoctorDto, Doctor>().ForMember(e => e.User, act => act.MapFrom(src => src));
    }
}
