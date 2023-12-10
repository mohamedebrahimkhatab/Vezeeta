namespace Vezeeta.Core.Contracts.AppointmentDtos;

public class DoctorAppointmentDto
{
    public decimal Price { get; set; }
    public List<DayDto>? Days { get; set; }
}
