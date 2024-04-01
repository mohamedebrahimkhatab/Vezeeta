using Vezeeta.Core.Enums;
using Vezeeta.Core.Models.Identity;

namespace Vezeeta.Core.Models;

public class Booking : BaseEntity
{
    public BookingStatus BookingStatus { get; set; }
    public string? DiscountCode { get; set; }
    public decimal FinalPrice { get; set; }

    public int DoctorId { get; set; }
    public Doctor Doctor { get; set; } = null!;

    public int PatientId { get; set; }
    public ApplicationUser Patient { get; set; } = null!;

    public int AppointmentTimeId { get; set; }
    public AppointmentTime AppointmentTime { get; set; } = null!;
    public DateTime AppointmentRealTime { get; set; }
}
      