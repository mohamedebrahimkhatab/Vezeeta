using Microsoft.AspNetCore.Identity;
using System.Runtime.CompilerServices;
using Vezeeta.Core.Enums;

namespace Vezeeta.Core.Models.Identity;

public class ApplicationUser : IdentityUser<int>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    public string? PhotoPath { get; set; }

    public Gender? Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
}
