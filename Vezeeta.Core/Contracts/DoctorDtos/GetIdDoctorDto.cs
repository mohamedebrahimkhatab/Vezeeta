﻿namespace Vezeeta.Core.Contracts.DoctorDtos;

public class GetIdDoctorDto
{
    public string? PhotoPath { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Specialize { get; set; }
    public string? Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
}
