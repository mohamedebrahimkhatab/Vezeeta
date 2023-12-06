using System.Security.Permissions;

namespace Vezeeta.Core.Contracts.BookingDtos;

public class BookBookingDto
{
    public string? DiscountCode { get; set; }
    public int PatientId { get; set; }
    public int AppointmentTimeId { get; set; }
}
