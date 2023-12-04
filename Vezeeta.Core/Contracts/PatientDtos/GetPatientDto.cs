namespace Vezeeta.Core.Contracts.PatientDtos;

public class GetPatientDto
{
    public string? PhotoPath { get; set; }
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
}
