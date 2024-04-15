using AutoMapper;
using Vezeeta.Core.Models;
using Vezeeta.Core.Contracts.BookingDtos;
using Vezeeta.Core.Contracts.PatientDtos;

namespace Vezeeta.Core.Mapping;

public class BookingProfile : Profile
{
    public BookingProfile()
    {
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
                                                 .ForMember(e => e.Appointment, act => act.MapFrom(e => e.AppointmentRealTime))
                                                 .ForMember(e => e.Age, act => act.MapFrom(e
                                                 => e.Patient.DateOfBirth.AddYears(DateTime.Now.Year - e.Patient.DateOfBirth.Year) > DateTime.Now ?
                                                        DateTime.Now.Year - e.Patient.DateOfBirth.Year - 1 : DateTime.Now.Year - e.Patient.DateOfBirth.Year));
    }
}
