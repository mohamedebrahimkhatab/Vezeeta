using Vezeeta.Core.Models;

namespace Vezeeta.Services.Interfaces;

public interface IAppointmentService
{
    Task AddAppointmentsAndPrice(int doctorId, decimal price, List<Appointment> appointments);

    Task<AppointmentTime?> GetAppointmentTime(int id);
    Task UpdateAppointmentTime(AppointmentTime appointmentTime);
    Task DeleteAppointmentTime(AppointmentTime appointmentTime);
    Task<int> GetDoctorId(int userId);

}
