using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Vezeeta.Core.Enums;
using Vezeeta.Core.Models;
using Vezeeta.Data.Repositories.UnitOfWork;
using Vezeeta.Services.DomainServices.Interfaces;

namespace Vezeeta.Services.DomainServices.Services;

public class AppointmentService : IAppointmentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDoctorService _doctorService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AppointmentService(IUnitOfWork unitOfWork, IDoctorService doctorService, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _doctorService = doctorService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task AddAppointmentsAndPrice(int doctorId, decimal price, List<Appointment> appointments)
    {
        var userId = _httpContextAccessor.HttpContext.User.FindFirstValue("userId");
        Doctor doctor = await _unitOfWork.Doctors.FindWithCriteriaAndIncludesAsync(e => e.Id == doctorId) ?? new();
        doctor.Price = price;
        await _unitOfWork.Doctors.SaveChanges();
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
        await _unitOfWork.Appointments.SaveChanges();
    }

    private async Task UpdateAppointment(Appointment appointment)
    {
        _unitOfWork.Appointments.Update(appointment);
        await _unitOfWork.Appointments.SaveChanges();
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
        await _unitOfWork.AppointmentTimes.SaveChanges();
    }

    public async Task DeleteAppointmentTime(AppointmentTime appointmentTime)
    {
        var check = await _unitOfWork.Bookings.CountAsync(e => e.AppointmentTimeId == appointmentTime.Id && (e.BookingStatus.Equals(BookingStatus.Completed) || e.BookingStatus.Equals(BookingStatus.Pending)));
        if (check != 0)
        {
            throw new InvalidOperationException("there is Comleted or Pending Booking/s in this time");
        }
        _unitOfWork.AppointmentTimes.Delete(appointmentTime);
        await _unitOfWork.AppointmentTimes.SaveChanges();
    }

    public async Task<int> GetDoctorId(int userId)
    {
        return await _doctorService.GetDoctorId(userId);
    }
}
