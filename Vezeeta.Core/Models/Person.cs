using System.ComponentModel.DataAnnotations;
using Vezeeta.Core.Enums;

namespace Vezeeta.Core.Models;

public class Person : BaseEntity
{
    public string? FirstName { get; set; }
    
    public string? LastName { get; set;}

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? PhotoPath { get; set; }

    public Gender? Gender { get; set; }

    public DateTime? DateOfBirth { get; set; }
}
