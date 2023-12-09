using Vezeeta.Core.Contracts.AppointmentDtos;
using Vezeeta.Core.Models;

namespace Vezeeta.Core.Services;

public interface IAppointmentService
{
    Task AddAppointmentsAndPrice(int doctorId, decimal price, List<Appointment> appointments);
    Task UpdateTime(AppointmentTime appointmentTime);

    Task<AppointmentTime?> GetAppointmentTime(int id);
    Task DeleteAppointmentTime(AppointmentTime appointmentTime);
}
