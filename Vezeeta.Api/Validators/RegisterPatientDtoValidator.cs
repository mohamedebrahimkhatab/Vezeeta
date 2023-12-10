using FluentValidation;
using Vezeeta.Core.Contracts.PatientDtos;

namespace Vezeeta.Api.Validators;

public class RegisterPatientDtoValidator : AbstractValidator<RegisterPatientDto>
{
    public RegisterPatientDtoValidator()
    {
        RuleFor(e => e.FirstName).NotEmpty();
        RuleFor(e => e.LastName).NotEmpty();
        RuleFor(e => e.Email).EmailAddress();
        RuleFor(e => e.Phone).NotEmpty();
        RuleFor(e => e.Gender).IsInEnum();
        RuleFor(e => e.DateOfBirth).LessThanOrEqualTo(DateTime.Now.AddYears(-10));
        RuleFor(e => e.Password).MinimumLength(8);
    }
}
