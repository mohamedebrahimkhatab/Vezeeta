using Vezeeta.Services.Utilities;
using Vezeeta.Core.Contracts.AppointmentDtos;

namespace Vezeeta.Services.DomainServices.Interfaces;

public interface IAppointmentService
{
    Task<ServiceResponse> AddAppointmentsAndPrice(DoctorAppointmentDto dto);
    Task<ServiceResponse> UpdateAppointmentTime(UpdateTimeDto timeDto);
    Task<ServiceResponse> DeleteAppointmentTime(int id);
}
