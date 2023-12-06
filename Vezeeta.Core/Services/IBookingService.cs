using Vezeeta.Core.Models;

namespace Vezeeta.Core.Services;

public interface IBookingService
{
    Task Book(Booking booking, Coupon? coupon);
    Task<AppointmentTime?> GetAppointmentTime(int appointmentTimeId);
    Task<Coupon?> GetCoupon(string discountCode);
}
