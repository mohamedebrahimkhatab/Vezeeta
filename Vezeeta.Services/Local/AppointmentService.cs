using Vezeeta.Core;
using Vezeeta.Core.Contracts.AppointmentDtos;
using Vezeeta.Core.Models;
using Vezeeta.Core.Services;

namespace Vezeeta.Services.Local;

public class AppointmentService : IAppointmentService
{
    private readonly IUnitOfWork _unitOfWork;

    public AppointmentService(IUnitOfWork unitOfWork, IDoctorService doctorService)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task AddAppointmentsAndPrice(int doctorId, decimal price, List<Appointment> appointments)
    {
        Doctor? doctor = await _unitOfWork.Doctors.FindWithCriteriaAndIncludesAsync(e => e.Id == doctorId);
        doctor.Price = price;
        doctor.Appointments = appointments;
        await _unitOfWork.CommitAsync();
    }

    public async Task<AppointmentTime?> GetAppointmentTime(int id) => await _unitOfWork.AppointmentTimes.GetByIdAsync(id);

    public async Task UpdateTime(AppointmentTime appointmentTime)
    {
        var check = await _unitOfWork.AppointmentTimes.CountAsync(e => e.AppointmentId == appointmentTime.AppointmentId && e.Time == appointmentTime.Time);
        if (check != 0)
        {
            throw new InvalidOperationException("there is another appointment in this time");
        }
        _unitOfWork.AppointmentTimes.Update(appointmentTime);
        await _unitOfWork.CommitAsync();
    }

    public async Task DeleteAppointmentTime(AppointmentTime appointmentTime)
    {
        _unitOfWork.AppointmentTimes.Delete(appointmentTime);
        await _unitOfWork.CommitAsync();
    }
}
