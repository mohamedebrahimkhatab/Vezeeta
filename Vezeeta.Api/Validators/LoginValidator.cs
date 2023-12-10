using FluentValidation;
using Vezeeta.Core.Contracts.Authentication;

namespace Vezeeta.Api.Validators;

public class LoginValidator : AbstractValidator<Login>
{
    public LoginValidator()
    {
        RuleFor(e => e.Email).EmailAddress();
    }
}
