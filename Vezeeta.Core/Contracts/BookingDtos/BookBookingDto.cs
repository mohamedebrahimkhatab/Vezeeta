using System.Security.Permissions;

namespace Vezeeta.Core.Contracts.BookingDtos;

public class BookBookingDto
{
    public int AppointmentTimeId { get; set; }
    public string? DiscountCode { get; set; }
    public DateTime AppointmentRealTime { get; set; }
}
