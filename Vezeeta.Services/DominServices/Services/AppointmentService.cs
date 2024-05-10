using AutoMapper;
using Vezeeta.Core.Enums;
using Vezeeta.Core.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Vezeeta.Services.Utilities;
using Vezeeta.Data.Repositories.Interfaces;
using Vezeeta.Core.Contracts.AppointmentDtos;
using Vezeeta.Services.DomainServices.Interfaces;

namespace Vezeeta.Services.DomainServices.Services;

public class AppointmentService : IAppointmentService
{
    private readonly IBaseRepository<Appointment> _appointmentRepository;
    private readonly IBaseRepository<AppointmentTime> _timeRepository;
    private readonly IBaseRepository<Doctor> _doctorRepository;
    private readonly IBaseRepository<Booking> _bookingRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;

    public AppointmentService(IBaseRepository<Appointment> appointmentRepository,
        IBaseRepository<AppointmentTime> timeRepository,
        IBaseRepository<Doctor> doctorRepository,
        IBaseRepository<Booking> bookingRepository,
        IHttpContextAccessor httpContextAccessor,
        IMapper mapper)
    {
        _appointmentRepository = appointmentRepository;
        _timeRepository = timeRepository;
        _doctorRepository = doctorRepository;
        _bookingRepository = bookingRepository;
        _httpContextAccessor = httpContextAccessor;
        _mapper = mapper;
    }

    public async Task<ServiceResponse> AddAppointmentsAndPrice(DoctorAppointmentDto dto)
    {
        try
        {
            if (!int.TryParse(_httpContextAccessor.HttpContext.User.FindFirstValue("Id"), out int userId))
                return new(StatusCodes.Status400BadRequest, "Token is invalid : Invalid Id");

            var doctor = await _doctorRepository.GetByConditionAsync(e => e.ApplicationUser.Id == userId, nameof(Doctor.ApplicationUser));
            if (doctor == null)
                return new(StatusCodes.Status404NotFound, "Doctor is not found");

            var appointments = _mapper.Map<List<Appointment>>(dto.Days);

            doctor.Price = dto.Price;
            await _doctorRepository.UpdateAsync(doctor);

            foreach (Appointment appointment in appointments)
            {
                var existed = await _appointmentRepository.GetByConditionAsync(e =>
                                                                e.DoctorId == doctor.Id && e.Day == appointment.Day, nameof(Appointment.AppointmentTimes));
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
                    await _appointmentRepository.UpdateAsync(existed);
                }
                else
                {
                    appointment.DoctorId = doctor.Id;
                    await _appointmentRepository.AddAsync(appointment);
                }
            }
            return new(StatusCodes.Status201Created, null);
        }
        catch (Exception e)
        {
            return new(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    public async Task<ServiceResponse> UpdateAppointmentTime(UpdateTimeDto timeDto)
    {
        try
        {
            var time = await _timeRepository.GetByConditionAsync(e => e.Id == timeDto.Id, nameof(AppointmentTime.Appointment));
            if (time == null)
                return new(StatusCodes.Status404NotFound, "This Time is not found");

            if (!int.TryParse(_httpContextAccessor.HttpContext.User.FindFirstValue("Id"), out int userId))
                return new(StatusCodes.Status400BadRequest, "Token is invalid : Invalid Id");

            var doctor = await _doctorRepository.GetByConditionAsync(e => e.ApplicationUser.Id == userId, nameof(Doctor.ApplicationUser));
            if (doctor == null)
                return new(StatusCodes.Status404NotFound, "Doctor is not found");

            if (time.Appointment.DoctorId != doctor.Id)
                return new(StatusCodes.Status401Unauthorized, "You are unauthorized to change other doctors' appointments");

            time = _mapper.Map(timeDto, time);

            var checkTime = await _timeRepository.GetByConditionAsync(e => e.AppointmentId == time.AppointmentId && e.Time == time.Time);
            if (checkTime != null)
                return new(StatusCodes.Status400BadRequest, "There is another appointment time in this time");

            var booking = await _bookingRepository.GetByConditionAsync(e => e.AppointmentTimeId == time.Id && (e.BookingStatus.Equals(BookingStatus.Completed) || e.BookingStatus.Equals(BookingStatus.Pending)));
            if (booking != null)
                return new(StatusCodes.Status400BadRequest, "there is Comleted or Pending Booking/s in this time");

            await _timeRepository.UpdateAsync(time);
            return new(StatusCodes.Status204NoContent, null);
        }
        catch (Exception e)
        {
            return new(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    public async Task<ServiceResponse> DeleteAppointmentTime(int id)
    {
        try
        {
            var time = await _timeRepository.GetByConditionAsync(e => e.Id == id, nameof(AppointmentTime.Appointment), $"{nameof(AppointmentTime.Appointment)}.{nameof(AppointmentTime.Appointment.AppointmentTimes)}");
            if (time == null)
            {
                return new(StatusCodes.Status404NotFound, "This time is not Found");
            }

            if (!int.TryParse(_httpContextAccessor.HttpContext.User.FindFirstValue("Id"), out int userId))
                return new(StatusCodes.Status400BadRequest, "Token is invalid : Invalid Id");

            var doctor = await _doctorRepository.GetByConditionAsync(e => e.ApplicationUser.Id == userId, nameof(Doctor.ApplicationUser));
            if (doctor == null)
                return new(StatusCodes.Status404NotFound, "Doctor is not found");

            if (time.Appointment.DoctorId != doctor.Id)
            {
                return new(StatusCodes.Status401Unauthorized, "You are unauthorized to delete other doctors' appointments");
            }

            var check = await _bookingRepository.GetByConditionAsync(e => e.AppointmentTimeId == time.Id && (e.BookingStatus.Equals(BookingStatus.Completed) || e.BookingStatus.Equals(BookingStatus.Pending)));
            if (check != null)
            {
                return new(StatusCodes.Status400BadRequest, "there is Comleted or Pending Booking/s in this time");
            }
            await _timeRepository.DeleteAsync(time);
            if(time.Appointment.AppointmentTimes.Count == 0)
            {
                await _appointmentRepository.DeleteAsync(time.Appointment);
            }
            return new(StatusCodes.Status204NoContent, null);
        }
        catch (Exception e)
        {
            return new(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
}
