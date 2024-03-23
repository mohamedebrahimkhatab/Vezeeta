using FluentValidation;
using Vezeeta.Core.Contracts.BookingDtos;

namespace Vezeeta.Api.Validators
{
    public class BookBookingDtoValidator : AbstractValidator<BookBookingDto>
    {
        public BookBookingDtoValidator()
        {
            RuleFor(e => e.AppointmentRealTime).NotNull().GreaterThan(DateTime.Now);
        }
    }
}
