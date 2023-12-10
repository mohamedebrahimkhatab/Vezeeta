using FluentValidation;
using Vezeeta.Core.Contracts.AppointmentDtos;

namespace Vezeeta.Api.Validators;

public class UpdateTimeDtoValidator : AbstractValidator<UpdateTimeDto>
{
    public UpdateTimeDtoValidator()
    {
        TimeOnly time = new TimeOnly();
        RuleFor(e => e.Time).Must(e => TimeOnly.TryParse(e, out time)).WithMessage("Invalid time format please use correct 12-hour or 24-hour format");
    }
}
