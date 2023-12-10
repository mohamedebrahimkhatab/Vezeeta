using Vezeeta.Core.Contracts.AppointmentDtos;

namespace Vezeeta.Core.Contracts.DoctorDtos;

public class PatientGetDoctorDto
{
    public string? PhotoPath { get; set; }
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Specialize { get; set; }
    public decimal price { get; set; }
    public string? Gender { get; set; }
    public List<AppointmentDto>? Appointments { get; set; }
}
