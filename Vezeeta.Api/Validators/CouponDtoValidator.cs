using FluentValidation;
using Vezeeta.Core.Contracts.CouponDtos;
using Vezeeta.Core.Enums;

namespace Vezeeta.Api.Validators;

public class CouponDtoValidator : AbstractValidator<CouponDto>
{
    public CouponDtoValidator()
    {
        RuleFor(e => e.DiscountCode).NotEmpty();
        RuleFor(e => e.NumOfRequests).GreaterThanOrEqualTo(0);
        RuleFor(e => e.DiscountType).IsInEnum();
        RuleFor(e => e.Value).GreaterThanOrEqualTo(0).LessThan(100).When(e => e.DiscountType.Equals(DiscountType.Percentage));
        RuleFor(e => e.Value).GreaterThanOrEqualTo(0).When(e => e.DiscountType.Equals(DiscountType.Value));
    }
}
