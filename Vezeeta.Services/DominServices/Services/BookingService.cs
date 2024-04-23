using AutoMapper;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Vezeeta.Core.Contracts.BookingDtos;
using Vezeeta.Core.Contracts.PatientDtos;
using Vezeeta.Core.Enums;
using Vezeeta.Core.Models;
using Vezeeta.Core.Models.Identity;
using Vezeeta.Data.Parameters;
using Vezeeta.Data.Repositories.Interfaces;
using Vezeeta.Services.DomainServices.Interfaces;
using Vezeeta.Services.Utilities;

namespace Vezeeta.Services.DomainServices.Services;

public class BookingService : IBookingService
{
    private readonly IMapper _mapper;
    private readonly IBaseRepository<Doctor> _doctors;
    private readonly IBaseRepository<Coupon> _coupons;
    private readonly IBookingRepositoryExt _repositoryExt;
    private readonly IBaseRepository<AppointmentTime> _times;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IPaginationRepository<Booking> _repository;
    private readonly IBaseRepository<ApplicationUser> _patients;

    public BookingService(IMapper mapper, IPaginationRepository<Booking> repository,
                        IBaseRepository<Doctor> doctors,
                        IHttpContextAccessor httpContextAccessor,
                        IBaseRepository<ApplicationUser> patients,
                        IBaseRepository<AppointmentTime> times,
                        IBaseRepository<Coupon> coupons,
                        IBookingRepositoryExt repositoryExt)
    {

        _times = times;
        _mapper = mapper;
        _coupons = coupons;
        _repositoryExt = repositoryExt;
        _doctors = doctors;
        _patients = patients;
        _repository = repository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ServiceResponse> GetPatientBookings()
    {
        try
        {
            if (!int.TryParse(_httpContextAccessor.HttpContext.User.FindFirstValue("Id"), out int userId))
                return new(StatusCodes.Status400BadRequest, "Token is invalid : Invalid Id");

            var patient = await _patients.GetByConditionAsync(e => e.Id == userId);
            if (patient == null)
                return new(StatusCodes.Status404NotFound, "Patient is not found");


            var result = await _repository.FindByConditionAsync(e => e.PatientId == patient.Id,
                                                    "Doctor", "Doctor.Specialization", "Doctor.ApplicationUser",
                                                    "AppointmentTime", "AppointmentTime.Appointment");
            return new(StatusCodes.Status200OK, _mapper.Map<IEnumerable<PatientGetBookingDto>>(result));
        }
        catch (Exception e)
        {
            return new(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
    public async Task<ServiceResponse> GetDoctorBookings(BookingParameters parameters)
    {
        try
        {
            if (!int.TryParse(_httpContextAccessor.HttpContext.User.FindFirstValue("Id"), out int userId))
                return new(StatusCodes.Status400BadRequest, "Token is invalid : Invalid Id");

            var doctor = await _doctors.GetByConditionAsync(e => e.ApplicationUserId == userId);
            if (doctor == null)
                return new(StatusCodes.Status404NotFound, "Doctor is not found");

            var result = await _repository.SearchWithPagination(parameters,
                                                        e => e.DoctorId == doctor.Id && e.AppointmentTime.Appointment.Day == parameters.Day,
                                                        nameof(Booking.Patient),
                                                        nameof(Booking.AppointmentTime),
                                                        $"{nameof(Booking.AppointmentTime)}.{nameof(AppointmentTime.Appointment)}");
            return new(StatusCodes.Status200OK, _mapper.Map<IEnumerable<DoctorGetPatientDto>>(result.Data));
        }
        catch (Exception e)
        {
            return new(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    public async Task<ServiceResponse> GetReserved()
    {
        var result = await _repositoryExt.GetReserved();
        return new(StatusCodes.Status200OK, result);
    }

    public async Task<ServiceResponse> Book(BookBookingDto bookingDto)
    {
        try
        {
            if (!int.TryParse(_httpContextAccessor.HttpContext.User.FindFirstValue("Id"), out int userId))
                return new(StatusCodes.Status400BadRequest, "Token is invalid : Invalid Id");

            var patient = await _patients.GetByConditionAsync(e => e.Id == userId);
            if (patient == null)
                return new(StatusCodes.Status404NotFound, "Patient is not found");

            AppointmentTime? appointmentTime = await _times.GetByConditionAsync(e => e.Id == bookingDto.AppointmentTimeId, nameof(appointmentTime.Appointment));
            if (appointmentTime == null)
                return new(StatusCodes.Status404NotFound, "this time doesn't exist");

            var booking = _mapper.Map<Booking>(bookingDto);

            var coupon = await _coupons.GetByConditionAsync(e => e.DiscountCode == booking.DiscountCode);
            if (coupon == null && !string.IsNullOrEmpty(bookingDto.DiscountCode))
                return new(StatusCodes.Status404NotFound, "this Discount code coupon doesn't exist");


            booking.AppointmentTime = appointmentTime;

            if (booking.AppointmentTime.Appointment.Day.ToString() != booking.AppointmentRealTime.DayOfWeek.ToString())
                throw new InvalidOperationException("There is a mismatch between doctor's appointment day and the day you requested");

            if (booking.AppointmentTime.Time.ToString() != booking.AppointmentRealTime.ToShortTimeString())
                throw new InvalidOperationException("There is a mismatch between doctor's appointment time and the time you requested");

            Booking? checkBooking = await _repository.GetByConditionAsync(e =>
                                    e.AppointmentRealTime == booking.AppointmentRealTime &&
                                    e.BookingStatus.Equals(BookingStatus.Pending));
            if (checkBooking != null)
            {
                return new(StatusCodes.Status400BadRequest, "This time is already booked");
            }

            Doctor? doctor = await _doctors.GetByConditionAsync(e => e.Id == booking.AppointmentTime.Appointment.DoctorId);
            if (doctor == null)
                return new(StatusCodes.Status404NotFound, "Doctor is not found");

            if (doctor.Price == null || doctor.Price == 0)
                return new(StatusCodes.Status406NotAcceptable, "doctor didn't set the price");

            if (coupon == null)
                booking.FinalPrice = (decimal)doctor.Price;
            else
            {
                int patientReqs = (await _repository.FindByConditionAsync(e =>
                                e.PatientId == patient.Id && e.BookingStatus.Equals(BookingStatus.Completed))).Count();
                if (patientReqs < coupon.NumOfRequests)
                    return new(StatusCodes.Status406NotAcceptable, $"you must have {coupon.NumOfRequests} completed requests to apply this discount code");

                if (coupon.DiscountType.Equals(DiscountType.Percentage))
                    booking.FinalPrice = (decimal)(doctor.Price - doctor.Price * (coupon.Value / 100));
                else
                    booking.FinalPrice = (decimal)doctor.Price - coupon.Value;
            }

            booking.BookingStatus = BookingStatus.Pending;
            booking.DoctorId = doctor.Id;
            booking.PatientId = patient.Id;
            await _repository.AddAsync(booking);
            return new(StatusCodes.Status201Created, booking.AppointmentRealTime);
        }
        catch (Exception e)
        {
            return new(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    public async Task<ServiceResponse> ConfirmCheckUp(int id)
    {
        try
        {
            if (!int.TryParse(_httpContextAccessor.HttpContext.User.FindFirstValue("Id"), out int userId))
                return new(StatusCodes.Status400BadRequest, "Token is invalid : Invalid Id");

            var doctor = await _doctors.GetByConditionAsync(e => e.ApplicationUserId == userId);
            if (doctor == null)
                return new(StatusCodes.Status404NotFound, "Doctor is not found");

            Booking? booking = await _repository.GetByConditionAsync(e => e.Id == id);
            if (booking == null)
                return new(StatusCodes.Status404NotFound, "This booking is not found");
            
            if (booking.BookingStatus.Equals(BookingStatus.Cancelled))
                return new(StatusCodes.Status406NotAcceptable, "Can not confirm check up for cancelled bookings");
            
            if (booking.BookingStatus.Equals(BookingStatus.Completed))
                return new(StatusCodes.Status406NotAcceptable, "This booking is already checked app");
            
            if (booking.DoctorId != doctor.Id)
                return new(StatusCodes.Status401Unauthorized, "Your not authrized to confirm other doctos' bookings");
            
            booking.BookingStatus = BookingStatus.Completed;
            await _repository.UpdateAsync(booking);
            return new(StatusCodes.Status204NoContent, null);
        }
        catch (Exception e)
        {
            return new(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
    public async Task<ServiceResponse> Cancel(int id)
    {
        try
        {
            if (!int.TryParse(_httpContextAccessor.HttpContext.User.FindFirstValue("Id"), out int userId))
                return new(StatusCodes.Status400BadRequest, "Token is invalid : Invalid Id");

            var patient = await _patients.GetByConditionAsync(e => e.Id == userId);
            if (patient == null)
                return new(StatusCodes.Status404NotFound, "Patient is not found");

            Booking? booking = await _repository.GetByConditionAsync(e => e.Id == id);
            if (booking == null)
                return new(StatusCodes.Status404NotFound, "This booking is not found");
            
            if (booking.BookingStatus.Equals(BookingStatus.Cancelled))
                return new(StatusCodes.Status406NotAcceptable, "This booking is already Cancelled");
            
            if (booking.BookingStatus.Equals(BookingStatus.Completed))
                return new(StatusCodes.Status406NotAcceptable, "Can not Cancel for completed bookings");
            
            if (booking.PatientId != patient.Id)
                return new(StatusCodes.Status401Unauthorized, "Your not authrized to confirm other patients' bookings");
            
            booking.BookingStatus = BookingStatus.Cancelled;
            await _repository.UpdateAsync(booking);
            return new(StatusCodes.Status204NoContent, null);
        }
        catch (Exception e)
        {
            return new(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
}
