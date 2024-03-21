using AutoMapper;
using Azure;
using Vezeeta.Core;
using Vezeeta.Core.Contracts.DoctorDtos;
using Vezeeta.Core.Contracts;
using Vezeeta.Core.Enums;
using Vezeeta.Core.Models;
using Vezeeta.Core.Models.Identity;
using Vezeeta.Core.Services;
using Vezeeta.Core.Contracts.PatientDtos;

namespace Vezeeta.Services.Local;

public class BookingService : IBookingService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDoctorService _doctorService;
    private readonly IMapper _mapper;

    public BookingService(IUnitOfWork unitOfWork, IDoctorService doctorService, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _doctorService = doctorService;
        _mapper = mapper;
    }

    public async Task Book(Booking booking, Coupon? coupon)
    {
        Booking? checkBooking = await _unitOfWork.Bookings.FindWithCriteriaAndIncludesAsync(e => 
                                e.AppointmentTimeId == booking.AppointmentTimeId && 
                                (e.BookingStatus.Equals(BookingStatus.Pending) || e.BookingStatus.Equals(BookingStatus.Completed)));
        if (checkBooking != null)
        {
            throw new InvalidOperationException("appointment time is already booked by another Patient");
        }
        Doctor? doctor = await _unitOfWork.Doctors.GetByIdAsync(booking.AppointmentTime.Appointment.DoctorId);
        if (doctor == null)
        {
            throw new InvalidOperationException("doctor doesn't exist");
        }
        if(doctor.Price == null)
        {
            throw new InvalidOperationException("doctor didn't set the price");
        }
        booking.FinalPrice = await GetFinalPrice(booking.PatientId,(decimal)doctor.Price, coupon);
        booking.BookingStatus = BookingStatus.Pending;
        booking.DoctorId = doctor.Id;
        await _unitOfWork.Bookings.AddAsync(booking);
        await _unitOfWork.CommitAsync();
    }

    private async Task<decimal> GetFinalPrice(int patientId, decimal price, Coupon? coupon)
    {
        if(coupon == null)
        {
            return price;
        }
        int patientReqs = await _unitOfWork.Bookings.CountAsync(e => 
                        e.PatientId == patientId && e.BookingStatus.Equals(BookingStatus.Completed));
        if(patientReqs < coupon.NumOfRequests)
        {
            throw new InvalidOperationException($"you must have {coupon.NumOfRequests} completed requests to apply this discount code");
        }
        if(coupon.DiscountType.Equals(DiscountType.Percentage))
        {
            return price - price * (coupon.Value / 100);
        }
        return price - coupon.Value;
    }

    public async Task<AppointmentTime?> GetAppointmentTime(int appointmentTimeId)
    {
        return await _unitOfWork.AppointmentTimes.FindWithCriteriaAndIncludesAsync(e => e.Id == appointmentTimeId, nameof(AppointmentTime.Appointment));
    }

    public async Task<Coupon?> GetCoupon(string discountCode)
    {
        return await _unitOfWork.Coupons.FindWithCriteriaAndIncludesAsync(e => e.DiscountCode == discountCode);
    }

    public async Task<Booking?> GetById(int id)
    {
        return await _unitOfWork.Bookings.GetByIdAsync(id);
    }

    public async Task ConfirmCheckUp(Booking booking)
    {
        if (booking.BookingStatus.Equals(BookingStatus.Cancelled))
        {
            throw new InvalidOperationException("can not confirm check up for cancelled bookings");
        }
        if (booking.BookingStatus.Equals(BookingStatus.Completed))
        {
            throw new InvalidOperationException("this booking is already checked app");
        }
        booking.BookingStatus = BookingStatus.Completed;
        await UpdateBooking(booking);
    }
    public async Task Cancel(Booking booking)
    {
        if (booking.BookingStatus.Equals(BookingStatus.Cancelled))
        {
            throw new InvalidOperationException("this booking is already Cancelled");
        }
        if (booking.BookingStatus.Equals(BookingStatus.Completed))
        {
            throw new InvalidOperationException("can not Cancel for completed bookings");
        }
        booking.BookingStatus = BookingStatus.Cancelled;
        await UpdateBooking(booking);
    }
    
    private async Task UpdateBooking(Booking booking)
    {
        _unitOfWork.Bookings.Update(booking);
        await _unitOfWork.CommitAsync();
    }

    public async Task<IEnumerable<Booking>> GetPatientBookings(int patientId)
    {
        return await _unitOfWork.Bookings.FindAllWithCriteriaAndIncludesAsync(e => e.PatientId == patientId,
                                                "Doctor", "Doctor.Specialization", "Doctor.ApplicationUser",
                                                "AppointmentTime", "AppointmentTime.Appointment");
    }

    public async Task<PaginationResult<DoctorGetPatientDto>> GetDoctorBookings(int doctorId, Days day, int pageSize, int pageNumber)
    {
        var totalCount = await _unitOfWork.Bookings.CountAsync(e => e.DoctorId == doctorId);
        var totalPages = totalCount / pageSize;
        if (totalCount % pageSize != 0)
        {
            totalPages++;
        }
        if (pageNumber > totalPages)
        {
            pageNumber = totalPages;
        }
        int skip = (pageNumber - 1) * pageSize;
        var query = await _unitOfWork.Bookings.FindAllWithCriteriaPagenationAndIncludesAsync(e => e.DoctorId == doctorId && e.AppointmentTime.Appointment.Day == day, skip, pageSize, nameof(Booking.Patient),
                                                                    nameof(Booking.AppointmentTime), $"{nameof(Booking.AppointmentTime)}.Appointment");
        return new PaginationResult<DoctorGetPatientDto>
        {
            TotalCount = totalCount,
            TotalPages = totalPages,
            CurrentPage = pageNumber,
            PageSize = pageSize,
            Data = _mapper.Map<IEnumerable<DoctorGetPatientDto>>(query)
        };

    }
    public async Task<int> GetDoctorId(int userId)
    {
        return await _doctorService.GetDoctorId(userId);
    }
}
