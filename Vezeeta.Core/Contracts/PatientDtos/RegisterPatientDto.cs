using Microsoft.AspNetCore.Http;
using Vezeeta.Core.Enums;

namespace Vezeeta.Core.Contracts.PatientDtos;

public class RegisterPatientDto
{
    public IFormFile? Image { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public Gender? Gender { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? Password { get; set; }
}
