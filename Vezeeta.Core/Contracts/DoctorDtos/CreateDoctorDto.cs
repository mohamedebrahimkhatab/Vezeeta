using Vezeeta.Core.Enums;

namespace Vezeeta.Core.Contracts.DoctorDtos;

public class CreateDoctorDto
{
    public string? PhotoPath { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public Gender? Gender { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public int SpecializationId { get; set; }
}
