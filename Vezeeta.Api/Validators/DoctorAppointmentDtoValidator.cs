using FluentValidation;
using Vezeeta.Core.Contracts.AppointmentDtos;

namespace Vezeeta.Api.Validators;

public class DoctorAppointmentDtoValidator : AbstractValidator<DoctorAppointmentDto>
{
    public DoctorAppointmentDtoValidator()
    {
        RuleForEach(e => e.Days).SetValidator(new DayDtoValidator());
    }
}

public class DayDtoValidator : AbstractValidator<DayDto>
{
    public DayDtoValidator()
    {
        TimeOnly time = new TimeOnly();
        RuleFor(e => e.Day).IsInEnum();
        RuleForEach(e => e.Times).Must(e => TimeOnly.TryParse(e, out time)).WithMessage("Invalid time format please use correct 12-hour or 24-hour format");
    }
}

