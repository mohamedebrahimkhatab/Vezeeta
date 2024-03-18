using Vezeeta.Core.Enums;
using Vezeeta.Core.Models;

namespace Vezeeta.Core.Contracts.DoctorDtos;

public class AdminGetDoctorDto
{
    public int Id { get; set; }
    public string? PhotoPath { get; set; }
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Specialize { get; set; }
    public string? Gender { get; set; }
}
