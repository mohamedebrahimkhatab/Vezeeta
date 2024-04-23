using AutoMapper;
using Vezeeta.Core.Enums;
using Vezeeta.Core.Models;
using Vezeeta.Core.Models.Identity;
using Vezeeta.Core.Contracts.DoctorDtos;

namespace Vezeeta.Core.Mapping;

public class DoctorProfile : Profile
{
	public DoctorProfile()
	{
        CreateMap<CreateDoctorDto, ApplicationUser>().ForMember(e => e.UserName, act => act.MapFrom(e => e.Email))
                                                     .ForMember(e => e.UserType, act => act.MapFrom(e => UserType.Doctor))
                                                     .ForMember(e => e.PhoneNumber, act => act.MapFrom(e => e.Phone));

        CreateMap<CreateDoctorDto, Doctor>().ForMember(e => e.ApplicationUser, act => act.MapFrom(e => e));

        CreateMap<UpdateDoctorDto, ApplicationUser>().ForMember(e => e.UserName, act => act.MapFrom(e => e.Email))
                                                     .ForMember(e => e.UserType, act => act.MapFrom(e => UserType.Doctor))
                                                     .ForMember(e => e.PhoneNumber, act => act.MapFrom(e => e.Phone));

        CreateMap<UpdateDoctorDto, Doctor>().ForMember(e => e.Id, act => act.MapFrom(e => e.DoctorId)).ForMember(e => e.ApplicationUser, act => act.MapFrom(e => e));

        CreateMap<Doctor, AdminGetDoctorDto>().ForMember(e => e.FullName, act => act.MapFrom(e => e.ApplicationUser.FirstName + ' ' + e.ApplicationUser.LastName))
                                         .ForMember(e => e.Specialize, act => act.MapFrom(e => e.Specialization.Name))
                                         .ForMember(e => e.Email, act => act.MapFrom(e => e.ApplicationUser.Email))
                                         .ForMember(e => e.PhotoPath, act => act.MapFrom(e => e.ApplicationUser.PhotoPath))
                                         .ForMember(e => e.Phone, act => act.MapFrom(e => e.ApplicationUser.PhoneNumber))
                                         .ForMember(e => e.Gender, act => act.MapFrom(e => e.ApplicationUser.Gender));

        CreateMap<Doctor, PatientGetDoctorDto>().ForMember(e => e.FullName, act => act.MapFrom(e => e.ApplicationUser.FirstName + ' ' + e.ApplicationUser.LastName))
                                         .ForMember(e => e.Specialize, act => act.MapFrom(e => e.Specialization.Name))
                                         .ForMember(e => e.Email, act => act.MapFrom(e => e.ApplicationUser.Email))
                                         .ForMember(e => e.PhotoPath, act => act.MapFrom(e => e.ApplicationUser.PhotoPath))
                                         .ForMember(e => e.Phone, act => act.MapFrom(e => e.ApplicationUser.PhoneNumber))
                                         .ForMember(e => e.Gender, act => act.MapFrom(e => e.ApplicationUser.Gender));

        CreateMap<Doctor, GetIdDoctorDto>().ForMember(e => e.FullName, act => act.MapFrom(e => e.ApplicationUser.FirstName + ' ' + e.ApplicationUser.LastName))
                                         .ForMember(e => e.Specialize, act => act.MapFrom(e => e.Specialization.Name))
                                         .ForMember(e => e.Email, act => act.MapFrom(e => e.ApplicationUser.Email))
                                         .ForMember(e => e.PhotoPath, act => act.MapFrom(e => e.ApplicationUser.PhotoPath))
                                         .ForMember(e => e.Phone, act => act.MapFrom(e => e.ApplicationUser.PhoneNumber))
                                         .ForMember(e => e.DateOfBirth, act => act.MapFrom(e => e.ApplicationUser.DateOfBirth))
                                         .ForMember(e => e.Gender, act => act.MapFrom(e => e.ApplicationUser.Gender));
    }
}
