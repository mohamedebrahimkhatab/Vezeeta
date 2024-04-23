using Vezeeta.Core.Consts;
using Vezeeta.Api.Validators;
using Vezeeta.Data.Parameters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Vezeeta.Core.Contracts.BookingDtos;
using Vezeeta.Services.DomainServices.Interfaces;

namespace Vezeeta.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController] 
[Authorize]
public class BookingsController : ControllerBase
{
    private readonly IBookingService _bookingService;

    public BookingsController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    [HttpGet]
    [Authorize(Roles = UserRoles.Patient)]
    public async Task<IActionResult> PatientGetAll()
    {
        var result = await _bookingService.GetPatientBookings();
        return StatusCode(result.StatusCode, result.Body);
    }

    [HttpGet]
    public async Task<IActionResult> GetReserved()
    {
        var result = await _bookingService.GetReserved();
        return StatusCode(result.StatusCode, result.Body);
    }

    [HttpGet]
    [Authorize(Roles = UserRoles.Doctor)]
    public async Task<IActionResult> DoctorGetAll([FromQuery]BookingParameters parameters)
    {
        var result = await _bookingService.GetDoctorBookings(parameters);
        return StatusCode(result.StatusCode, result.Body);
    }

    [HttpPost]
    [Authorize(Roles = UserRoles.Patient)]
    public async Task<IActionResult> Book(BookBookingDto bookingDto)
    {
        try
        {
            var validator = new BookBookingDtoValidator();
            var validate = await validator.ValidateAsync(bookingDto);
            if (!validate.IsValid)
            {
                return BadRequest(validate.Errors.Select(e => e.ErrorMessage));
            }

            var result = await _bookingService.Book(bookingDto);
            return StatusCode(result.StatusCode, result.Body);
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
            var result = await _bookingService.ConfirmCheckUp(id);
            return StatusCode(result.StatusCode, result.Body);
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
            var result = await _bookingService.Cancel(id);
            return StatusCode(result.StatusCode, result.Body);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

}
