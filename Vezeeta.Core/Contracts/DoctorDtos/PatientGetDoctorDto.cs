using Vezeeta.Core.Contracts.AppointmentDtos;
using Vezeeta.Core.Models;

namespace Vezeeta.Core.Contracts.DoctorDtos;

public class PatientGetDoctorDto 
{
    public int Id { get; set; }
    public string? PhotoPath { get; set; }
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Specialize { get; set; }
    public decimal price { get; set; }
    public string? Gender { get; set; }
    public List<AppointmentDto>? Appointments { get; set; }
}
