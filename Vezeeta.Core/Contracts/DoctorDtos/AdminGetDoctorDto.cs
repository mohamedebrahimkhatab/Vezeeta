using Vezeeta.Core.Enums;

namespace Vezeeta.Core.Contracts.DoctorDtos;

public class AdminGetDoctorDto
{
    public string? PhotoPath { get; set; }
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Specialize { get; set; }
    public string? Gender { get; set; }
}
