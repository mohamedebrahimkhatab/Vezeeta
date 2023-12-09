using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vezeeta.Core.Contracts.BookingDtos;
using Vezeeta.Core.Contracts.PatientDtos;
using Vezeeta.Core.Models;
using Vezeeta.Core.Models.Identity;
using Vezeeta.Core.Services;

namespace Vezeeta.Api.Controllers;

[Route("api/[controller]/[action]")]
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

    [HttpGet]
    public async Task<IActionResult> PatientGetAll()
    {
        int patientId = 4;
        IEnumerable<Booking> bookings = await _bookingService.GetPatientBookings(patientId);
        return Ok(_mapper.Map<List<PatientGetBookingDto>>(bookings));
    }

    [HttpGet]
    public async Task<IActionResult> DoctorGetAll()
    {
        int DoctorId = 3;
        IEnumerable<Booking> bookings = await _bookingService.GetDoctorBookings(DoctorId);
        return Ok(_mapper.Map<List<DoctorGetPatientDto>>(bookings.Select(e => e.Patient)));
    }

    [HttpPost]
    public async Task<IActionResult> Book(BookBookingDto bookingDto)
    {
        try
        {
            AppointmentTime? appointmentTime = await _bookingService.GetAppointmentTime(bookingDto.AppointmentTimeId);
            if (appointmentTime == null)
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
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpPut]
    public async Task<IActionResult> ConfirmCheckUp(int id)
    {
        Booking? booking = await _bookingService.GetById(id);
        if(booking == null)
        {
            return NotFound();
        }
        await _bookingService.ConfirmCheckUp(booking);
        return NoContent();
    }

    [HttpPut]
    public async Task<IActionResult> Cancel(int id)
    {
        Booking? booking = await _bookingService.GetById(id);
        if (booking == null)
        {
            return NotFound();
        }
        await _bookingService.Cancel(booking);
        return NoContent();
    }

}
