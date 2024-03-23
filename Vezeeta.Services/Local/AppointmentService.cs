using System.ComponentModel.DataAnnotations;
using Vezeeta.Core;
using Vezeeta.Core.Contracts.AppointmentDtos;
using Vezeeta.Core.Enums;
using Vezeeta.Core.Models;
using Vezeeta.Core.Services;

namespace Vezeeta.Services.Local;

public class AppointmentService : IAppointmentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDoctorService _doctorService;

    public AppointmentService(IUnitOfWork unitOfWork, IDoctorService doctorService)
    {
        _unitOfWork = unitOfWork;
        _doctorService = doctorService;
    }

    public async Task AddAppointmentsAndPrice(int doctorId, decimal price, List<Appointment> appointments)
    {
        Doctor? doctor = await _unitOfWork.Doctors.FindWithCriteriaAndIncludesAsync(e => e.Id == doctorId);
        doctor.Price = price;
        await _unitOfWork.CommitAsync();
        foreach (Appointment appointment in appointments)
        {
            var existed = await _unitOfWork.Appointments.FindWithCriteriaAndIncludesAsync(e =>
                                                            e.DoctorId == doctorId && e.Day == appointment.Day, nameof(Appointment.AppointmentTimes));
            if (existed != null)
            {
                IEnumerable<TimeOnly> addTimes = appointment.AppointmentTimes.Select(e => e.Time);
                foreach (var time in addTimes)
                {
                    if (!existed.AppointmentTimes.Any(e => e.Time == time))
                    {
                        existed.AppointmentTimes.Add(new AppointmentTime { Time = time });
                    }
                }
                await UpdateAppointment(existed);
            }
            else
            {
                appointment.DoctorId = doctorId;
                await AddAppointment(appointment);
            }
        }
    }

    private async Task AddAppointment(Appointment appointment)
    {
        await _unitOfWork.Appointments.AddAsync(appointment);
        await _unitOfWork.CommitAsync();
    }

    private async Task UpdateAppointment(Appointment appointment)
    {
        _unitOfWork.Appointments.Update(appointment);
        await _unitOfWork.CommitAsync();
    }


    public async Task<AppointmentTime?> GetAppointmentTime(int id) 
        => await _unitOfWork.AppointmentTimes.FindWithCriteriaAndIncludesAsync(e => e.Id == id, nameof(AppointmentTime.Appointment));

    public async Task UpdateAppointmentTime(AppointmentTime appointmentTime)
    {
        var check = await _unitOfWork.AppointmentTimes.CountAsync(e => e.AppointmentId == appointmentTime.AppointmentId && e.Time == appointmentTime.Time);
        if (check != 0)
        {
            throw new InvalidOperationException("there is another appointment in this time");
        }
        check = await _unitOfWork.Bookings.CountAsync(e => e.AppointmentTimeId == appointmentTime.Id && (e.BookingStatus.Equals(BookingStatus.Completed) || e.BookingStatus.Equals(BookingStatus.Pending)));
        if (check != 0)
        {
            throw new InvalidOperationException("there is Comleted or Pending Booking/s in this time");
        }
        _unitOfWork.AppointmentTimes.Update(appointmentTime);
        await _unitOfWork.CommitAsync();
    }

    public async Task DeleteAppointmentTime(AppointmentTime appointmentTime)
    {
        var check = await _unitOfWork.Bookings.CountAsync(e => e.AppointmentTimeId == appointmentTime.Id && (e.BookingStatus.Equals(BookingStatus.Completed) || e.BookingStatus.Equals(BookingStatus.Pending)));
        if (check != 0)
        {
            throw new InvalidOperationException("there is Comleted or Pending Booking/s in this time");
        }
        _unitOfWork.AppointmentTimes.Delete(appointmentTime);
        await _unitOfWork.CommitAsync();
    }

    public async Task<int> GetDoctorId(int userId)
    {
        return await _doctorService.GetDoctorId(userId);
    }
}
