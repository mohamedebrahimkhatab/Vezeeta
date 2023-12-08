using Vezeeta.Core.Models;
using Vezeeta.Core.Models.Identity;

namespace Vezeeta.Core.Services;

public interface IBookingService
{
    Task<Booking?> GetById(int id);
    Task Book(Booking booking, Coupon? coupon);
    Task<AppointmentTime?> GetAppointmentTime(int appointmentTimeId);
    Task<Coupon?> GetCoupon(string discountCode);
    Task ConfirmCheckUp(Booking booking);
    Task Cancel(Booking booking);
}
