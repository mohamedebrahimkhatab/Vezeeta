using Vezeeta.Core.Enums;
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
    Task<IEnumerable<Booking>> GetPatientBookings(int patientId);
    Task<IEnumerable<Booking>> GetDoctorBookings(int doctorId,Days day, int pageSize, int pageNumber);
    Task<int> GetDoctorId(int userId);
}
