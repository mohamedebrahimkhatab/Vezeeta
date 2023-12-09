namespace Vezeeta.Core.Contracts.PatientDtos;

public class DoctorGetPatientDto
{
    public string? PhotoPath { get; set; }
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Gender { get; set; }
    public int Age { get; set; }
}
