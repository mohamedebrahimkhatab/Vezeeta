using Vezeeta.Core;
using Vezeeta.Core.Enums;
using Vezeeta.Core.Models;
using Vezeeta.Core.Models.Identity;
using Vezeeta.Core.Services;

namespace Vezeeta.Services.Local;

public class BookingService : IBookingService
{
    private readonly IUnitOfWork _unitOfWork;

    public BookingService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Book(Booking booking, Coupon? coupon)
    {
        Booking? checkBooking = await _unitOfWork.Bookings.FindWithCriteriaAndIncludesAsync(e => 
                                e.AppointmentTimeId == booking.AppointmentTimeId && e.BookingStatus.Equals(BookingStatus.Pending));
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
        if(patientReqs >= coupon.NumOfRequests)
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
        booking.BookingStatus = BookingStatus.Completed;
        await UpdateBooking(booking);
    }
    public async Task Cancel(Booking booking)
    {
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
}
