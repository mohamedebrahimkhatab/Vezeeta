using AutoMapper;
using Vezeeta.Core.Enums;
using Vezeeta.Core.Models;
using Vezeeta.Core.Models.Identity;
using Vezeeta.Core.Contracts.DoctorDtos;
using Vezeeta.Core.Contracts.PatientDtos;
using Vezeeta.Core.Contracts.CouponDtos;
using Vezeeta.Core.Contracts.AppointmentDtos;
using Vezeeta.Core.Contracts.BookingDtos;
using System.Globalization;

namespace Vezeeta.Api.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        /******************* Doctor DTOS **********************************/
        CreateMap<CreateDoctorDto, ApplicationUser>().ForMember(e => e.UserName, act => act.MapFrom(e => e.Email))
                                                     .ForMember(e => e.UserType, act => act.MapFrom(e => UserType.Doctor))
                                                     .ForMember(e => e.PhoneNumber, act => act.MapFrom(e => e.Phone));
        CreateMap<CreateDoctorDto, Doctor>().ForMember(e => e.ApplicationUser, act => act.MapFrom(e => e));

        CreateMap<UpdateDoctorDto, ApplicationUser>().ForMember(e => e.UserName, act => act.MapFrom(e => e.Email))
                                                     .ForMember(e => e.UserType, act => act.MapFrom(e => UserType.Doctor))
                                                     .ForMember(e => e.PhoneNumber, act => act.MapFrom(e => e.Phone));
        CreateMap<UpdateDoctorDto, Doctor>().ForMember(e => e.Id, act => act.MapFrom(e => e.DoctorId)).ForMember(e => e.ApplicationUser, act => act.MapFrom(e => e));

        CreateMap<Doctor, GetDoctorDto>().ForMember(e => e.FullName, act => act.MapFrom(e => e.ApplicationUser.FirstName + ' ' + e.ApplicationUser.LastName))
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

        /******************* Patient DTOS **********************************/

        CreateMap<RegisterPatientDto, ApplicationUser>().ForMember(e => e.UserName, act => act.MapFrom(e => e.Email))
                                                        .ForMember(e => e.UserType, act => act.MapFrom(e => UserType.Patient))
                                                        .ForMember(e => e.PhoneNumber, act => act.MapFrom(e => e.Phone));

        CreateMap<ApplicationUser, GetPatientDto>().ForMember(e => e.FullName, act => act.MapFrom(e => e.FirstName + ' ' + e.LastName))
                                                   .ForMember(e => e.Phone, act => act.MapFrom(e => e.PhoneNumber));

        /******************* Coupon DTOS **********************************/

        CreateMap<CouponDto, Coupon>().ReverseMap();
        CreateMap<UpdateCouponDto, Coupon>();

        /******************* Appointment DTOS **********************************/

        CreateMap<string, AppointmentTime>().ForMember(e => e.Time, act => act.MapFrom(e => TimeOnly.Parse(e)));
        CreateMap<DayDto, Appointment>().ForMember(e => e.AppointmentTimes, act => act.MapFrom(e => e.Times));
        CreateMap<UpdateTimeDto, AppointmentTime>().ForMember(e => e.Time, act => act.MapFrom(e => TimeOnly.Parse(e.Time)));

        /******************* Booking DTOS **********************************/

        CreateMap<BookBookingDto, Booking>();
        CreateMap<Booking, PatientGetBookingDto>().ForMember(e => e.PhotoPath, act => act.MapFrom(e => e.Doctor.ApplicationUser.PhotoPath))
                                               .ForMember(e => e.Price, act => act.MapFrom(e => e.Doctor.Price))
                                               .ForMember(e => e.Specialize, act => act.MapFrom(e => e.Doctor.Specialization.Name))
                                               .ForMember(e => e.Day, act => act.MapFrom(e => e.AppointmentTime.Appointment.Day))
                                               .ForMember(e => e.Time, act => act.MapFrom(e => e.AppointmentTime.Time))
                                               .ForMember(e => e.DoctorName, act => act.MapFrom(e => $"{e.Doctor.ApplicationUser.FirstName} {e.Doctor.ApplicationUser.LastName}"));

        CreateMap<Booking, DoctorGetPatientDto>().ForMember(e => e.PatientName, act => act.MapFrom(e => e.Patient.FirstName + ' ' + e.Patient.LastName))
                                                 .ForMember(e => e.Phone, act => act.MapFrom(e => e.Patient.PhoneNumber))
                                                 .ForMember(e => e.PhotoPath, act => act.MapFrom(e => e.Patient.PhotoPath))
                                                 .ForMember(e => e.Email, act => act.MapFrom(e => e.Patient.Email))
                                                 .ForMember(e => e.Gender, act => act.MapFrom(e => e.Patient.Gender))
                                                 .ForMember(e => e.Appointment, act => act.MapFrom(e => e.AppointmentTime.Appointment.Day + " " + e.AppointmentTime.Time))
                                                 .ForMember(e => e.Age, act => act.MapFrom(e
                                                 => e.Patient.DateOfBirth.AddYears(DateTime.Now.Year - e.Patient.DateOfBirth.Year) > DateTime.Now ?
                                                        DateTime.Now.Year - e.Patient.DateOfBirth.Year - 1 : DateTime.Now.Year - e.Patient.DateOfBirth.Year));
    }
}
