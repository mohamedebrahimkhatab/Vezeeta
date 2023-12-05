namespace Vezeeta.Core.Contracts.AppointmentDtos;

public class DoctorAppointmentDto
{
    public int DoctorId { get; set; }
    public decimal Price { get; set; }
    public List<DayDto>? Days { get; set; }
}
