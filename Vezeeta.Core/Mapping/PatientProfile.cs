using AutoMapper;
using Vezeeta.Core.Enums;
using Vezeeta.Core.Models.Identity;
using Vezeeta.Core.Contracts.PatientDtos;

namespace Vezeeta.Core.Mapping;

public class PatientProfile : Profile
{
    public PatientProfile()
    {
        CreateMap<RegisterPatientDto, ApplicationUser>().ForMember(e => e.UserName, act => act.MapFrom(e => e.Email))
                                                        .ForMember(e => e.UserType, act => act.MapFrom(e => UserType.Patient))
                                                        .ForMember(e => e.PhoneNumber, act => act.MapFrom(e => e.Phone));

        CreateMap<ApplicationUser, GetPatientDto>().ForMember(e => e.FullName, act => act.MapFrom(e => e.FirstName + ' ' + e.LastName))
                                                   .ForMember(e => e.Phone, act => act.MapFrom(e => e.PhoneNumber));
    }
}
