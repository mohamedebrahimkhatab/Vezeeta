using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vezeeta.Core.Contracts.BookingDtos;
using Vezeeta.Core.Models;
using Vezeeta.Core.Services;

namespace Vezeeta.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookingsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IBookingService _bookingService;

    public BookingsController(IBookingService bookingService, IMapper mapper)
    {
        _mapper = mapper;
        _bookingService = bookingService;
    }

    [HttpPost]
    public async Task<IActionResult> Book(BookBookingDto bookingDto)
    {
        AppointmentTime? appointmentTime = await _bookingService.GetAppointmentTime(bookingDto.AppointmentTimeId);
        if(appointmentTime == null)
        {
            return NotFound("this time doesn't exist");
        }
        Coupon? coupon = null;
        if (!string.IsNullOrEmpty(bookingDto.DiscountCode))
        {
            coupon = await _bookingService.GetCoupon(bookingDto.DiscountCode);
            if (coupon == null)
            {
                return NotFound("this Discount code coupon doesn't exist");
            }
        }
        Booking booking = _mapper.Map<Booking>(bookingDto);
        booking.AppointmentTime = appointmentTime;
        await _bookingService.Book(booking, coupon);
        return Created();
    }
}
