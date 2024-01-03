using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Vezeeta.Core.Consts;
using Vezeeta.Core.Contracts.BookingDtos;
using Vezeeta.Core.Contracts.PatientDtos;
using Vezeeta.Core.Enums;
using Vezeeta.Core.Models;
using Vezeeta.Core.Services;

namespace Vezeeta.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController] 
[Authorize]
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
    [Authorize(Roles = UserRoles.Patient)]
    public async Task<IActionResult> PatientGetAll()
    {
        int patientId = int.Parse(User.FindFirstValue("Id"));
        IEnumerable<Booking> bookings = await _bookingService.GetPatientBookings(patientId);
        return Ok(_mapper.Map<List<PatientGetBookingDto>>(bookings));
    }

    [HttpGet]
    [Authorize(Roles = UserRoles.Doctor)]
    public async Task<IActionResult> DoctorGetAll(Days day, int? pageSize, int? pageNumber)
    {
        int userId = int.Parse(User.FindFirstValue("Id"));
        int doctorId = await _bookingService.GetDoctorId(userId);
        IEnumerable<Booking> bookings = await _bookingService.GetDoctorBookings(doctorId, day, pageSize ?? 10, pageNumber ?? 1);
        return Ok(_mapper.Map<List<DoctorGetPatientDto>>(bookings));
    }

    [HttpPost]
    [Authorize(Roles = UserRoles.Patient)]
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
            booking.PatientId = int.Parse(User.FindFirstValue(MyClaims.Id));
            await _bookingService.Book(booking, coupon);
            return Created();
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpPut]
    [Authorize(Roles = UserRoles.Doctor)]
    public async Task<IActionResult> ConfirmCheckUp(int id)
    {
        try
        {
            Booking? booking = await _bookingService.GetById(id);
            if (booking == null)
            {
                return NotFound();
            }
            await _bookingService.ConfirmCheckUp(booking);
            return NoContent();
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpPut]
    [Authorize(Roles = UserRoles.Patient)]
    public async Task<IActionResult> Cancel(int id)
    {
        try
        {
            Booking? booking = await _bookingService.GetById(id);
            if (booking == null)
            {
                return NotFound();
            }
            await _bookingService.Cancel(booking);
            return NoContent();
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

}
