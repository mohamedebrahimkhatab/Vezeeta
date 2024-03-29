using AutoMapper;
using Vezeeta.Core.Models;
using Vezeeta.Core.Contracts.AppointmentDtos;

namespace Vezeeta.Core.Mapping;

public class AppointmentProfile : Profile
{
    public AppointmentProfile()
    {
        CreateMap<string, AppointmentTime>().ForMember(e => e.Time, act => act.MapFrom(e => TimeOnly.Parse(e)));

        CreateMap<DayDto, Appointment>().ForMember(e => e.AppointmentTimes, act => act.MapFrom(e => e.Times));

        CreateMap<UpdateTimeDto, AppointmentTime>().ForMember(e => e.Time, act => act.MapFrom(e => TimeOnly.Parse(e.Time)));

        CreateMap<AppointmentTime, TimeDto>();

        CreateMap<Appointment, AppointmentDto>().ForMember(e => e.Times, act => act.MapFrom(e => e.AppointmentTimes));
    }
}
