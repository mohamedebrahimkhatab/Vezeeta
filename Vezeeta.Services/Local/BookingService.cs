using Vezeeta.Core;
using Vezeeta.Core.Enums;
using Vezeeta.Core.Models;
using Vezeeta.Core.Services;
using static Azure.Core.HttpHeader;

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
        Doctor? doctor = await _unitOfWork.Doctors.GetByIdAsync(booking.AppointmentTime.Appointment.DoctorId);
        if (doctor == null)
        {
            throw new InvalidOperationException("doctor doesn't exist");
        }
        if(doctor.Price == null)
        {
            throw new InvalidOperationException("doctor didn't set the price");
        }
        booking.FinalPrice = GetFinalPrice((decimal)doctor.Price, coupon);
        booking.ActualDateTime = GetActualDateTime(booking.AppointmentTime.Time, booking.AppointmentTime.Appointment.Day);
        booking.BookingStatus = BookingStatus.Pending;
        booking.DoctorId = doctor.Id;
        await _unitOfWork.Bookings.AddAsync(booking);
        await _unitOfWork.CommitAsync();
    }

    private DateTime GetActualDateTime(TimeOnly time, Days day)
    {
        Days today = (Days)((int)(DateTime.Now.DayOfWeek + 1) % 7);
        int dif = (day - today + 7) % 7;
        DateOnly date = DateOnly.FromDateTime(DateTime.Now.AddDays(dif));
        DateTime dateTime = date.ToDateTime(time);
        return dateTime;
    }

    private decimal GetFinalPrice(decimal price, Coupon? coupon)
    {
        if(coupon == null)
        {
            return price;
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
}
