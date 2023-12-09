namespace Vezeeta.Core.Contracts.BookingDtos;

public class PatientGetBookingDto
{
    public string? PhotoPath { get; set; }
    public string? DoctorName { get; set; }
    public string? Specialize { get; set; }
    public string? Day { get; set; }
    public string? Time { get; set; }
    public decimal Price { get; set; }
    public string? DiscountCode { get; set; }
    public decimal FinalPrice { get; set; }
    public string? BookingStatus { get; set; }
}
