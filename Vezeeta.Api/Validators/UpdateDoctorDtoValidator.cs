using FluentValidation;
using Vezeeta.Core.Contracts.DoctorDtos;

namespace Vezeeta.Api.Validators;

public class UpdateDoctorDtoValidator : AbstractValidator<UpdateDoctorDto>
{
    public UpdateDoctorDtoValidator()
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
