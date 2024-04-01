using Vezeeta.Core.Enums;
using Microsoft.AspNetCore.Identity;

namespace Vezeeta.Core.Models.Identity;

public class ApplicationUser : IdentityUser<int>
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;

    public string? PhotoPath { get; set; }

    public Gender? Gender { get; set; }

    public UserType? UserType { get; set; }

    public DateTime DateOfBirth { get; set; }
    public DateTime CreatedAt { get; set; }
}
