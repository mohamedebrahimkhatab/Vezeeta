using Vezeeta.Core.Contracts.AppointmentDtos;
using Vezeeta.Core.Models;
using Vezeeta.Services.Utilities;

namespace Vezeeta.Services.DomainServices.Interfaces;

public interface IAppointmentService
{
    Task<ServiceResponse> AddAppointmentsAndPrice(DoctorAppointmentDto dto);

    Task<ServiceResponse> UpdateAppointmentTime(UpdateTimeDto timeDto);
    Task<ServiceResponse> DeleteAppointmentTime(int id);

}
