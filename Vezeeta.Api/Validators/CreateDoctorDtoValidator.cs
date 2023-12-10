using FluentValidation;
using Vezeeta.Core.Contracts.DoctorDtos;

namespace Vezeeta.Api.Validators;

public class CreateDoctorDtoValidator : AbstractValidator<CreateDoctorDto>
{
    public CreateDoctorDtoValidator()
    {
        RuleFor(e => e.PhotoPath).NotEmpty();
        RuleFor(e => e.FirstName).NotEmpty();
        RuleFor(e => e.LastName).NotEmpty();
        RuleFor(e => e.Email).EmailAddress();
        RuleFor(e => e.Phone).NotEmpty();
        RuleFor(e => e.Gender).IsInEnum();
        RuleFor(e => e.DateOfBirth).LessThanOrEqualTo(DateTime.Now.AddYears(-23));
    }
}
