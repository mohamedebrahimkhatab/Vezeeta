using AutoMapper;
using Vezeeta.Core.Enums;
using Vezeeta.Core.Models;
using Vezeeta.Core.Models.Identity;
using Vezeeta.Core.Contracts.DoctorDtos;
using Vezeeta.Core.Contracts.PatientDtos;
using Vezeeta.Core.Contracts.CouponDtos;
using Vezeeta.Core.Contracts.AppointmentDtos;
using Vezeeta.Core.Contracts.BookingDtos;

namespace Vezeeta.Api.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        /******************* Doctor DTOS **********************************/
        CreateMap<CreateDoctorDto, ApplicationUser>().ForMember(e => e.UserName, act => act.MapFrom(src => src.Email))
                                                     .ForMember(e => e.UserType, act => act.MapFrom(src => UserType.Doctor))
                                                     .ForMember(e => e.PhoneNumber, act => act.MapFrom(src => src.Phone));
        CreateMap<CreateDoctorDto, Doctor>().ForMember(e => e.ApplicationUser, act => act.MapFrom(src => src));

        CreateMap<UpdateDoctorDto, ApplicationUser>().ForMember(e => e.UserName, act => act.MapFrom(src => src.Email))
                                                     .ForMember(e => e.UserType, act => act.MapFrom(src => UserType.Doctor))
                                                     .ForMember(e => e.PhoneNumber, act => act.MapFrom(src => src.Phone));
        CreateMap<UpdateDoctorDto, Doctor>().ForMember(e => e.Id, act => act.MapFrom(src => src.DoctorId)).ForMember(e => e.ApplicationUser, act => act.MapFrom(src => src));

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

        /******************* Patient DTOS **********************************/

        CreateMap<RegisterPatientDto, ApplicationUser>().ForMember(e => e.UserName, act => act.MapFrom(src => src.Email))
                                                        .ForMember(e => e.UserType, act => act.MapFrom(src => UserType.Patient))
                                                        .ForMember(e => e.PhoneNumber, act => act.MapFrom(src => src.Phone));

        CreateMap<ApplicationUser, GetPatientDto>().ForMember(e => e.FullName, act => act.MapFrom(src => src.FirstName + ' ' + src.LastName))
                                                   .ForMember(e => e.Phone, act => act.MapFrom(src => src.PhoneNumber));

        /******************* Coupon DTOS **********************************/

        CreateMap<CouponDto, Coupon>().ReverseMap();
        CreateMap<UpdateCouponDto, Coupon>();

        /******************* Appointment DTOS **********************************/

        CreateMap<string, AppointmentTime>().ForMember(e => e.Time, act => act.MapFrom(src => TimeOnly.Parse(src)));
        CreateMap<DayDto, Appointment>().ForMember(e => e.AppointmentTimes, act => act.MapFrom(src => src.Times));
        CreateMap<UpdateTimeDto, AppointmentTime>().ForMember(e => e.Time, act => act.MapFrom(src => TimeOnly.Parse(src.Time)));

        /******************* Booking DTOS **********************************/

        CreateMap<BookBookingDto, Booking>();
        CreateMap<Booking, PatientBookingDto>().ForMember(e => e.PhotoPath, act => act.MapFrom(e => e.Doctor.ApplicationUser.PhotoPath))
                                               .ForMember(e => e.Price, act => act.MapFrom(e => e.Doctor.Price))
                                               .ForMember(e => e.Specialize, act => act.MapFrom(e => e.Doctor.Specialization.Name))
                                               .ForMember(e => e.Day, act => act.MapFrom(e => e.AppointmentTime.Appointment.Day))
                                               .ForMember(e => e.Time, act => act.MapFrom(e => e.AppointmentTime.Time))
                                               .ForMember(e => e.DoctorName, act => act.MapFrom(e => $"{e.Doctor.ApplicationUser.FirstName} {e.Doctor.ApplicationUser.LastName}"));
    }
}
