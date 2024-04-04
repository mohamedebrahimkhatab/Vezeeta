using AutoMapper;
using Vezeeta.Core.Consts;
using Vezeeta.Api.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Vezeeta.Core.Contracts.AppointmentDtos;
using Vezeeta.Services.DomainServices.Interfaces;

namespace Vezeeta.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
[Authorize(Roles = UserRoles.Doctor)]
public class AppointmentsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IAppointmentService _appointmentService;

    public AppointmentsController(IMapper mapper, IAppointmentService appointmentService)
    {
        _mapper = mapper;
        _appointmentService = appointmentService;
    }

    [HttpPost]
    public async Task<IActionResult> Add(DoctorAppointmentDto dto)
    {
        try
        {
            var validator = new DoctorAppointmentDtoValidator();
            var validate = await validator.ValidateAsync(dto);
            if (!validate.IsValid)
            {
                return BadRequest(validate.Errors.Select(e => e.ErrorMessage));
            }
            await _appointmentService.AddAppointmentsAndPrice(dto);
            return Created();

        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateTime(UpdateTimeDto timeDto)
    {
        try
        {
            var validator = new UpdateTimeDtoValidator();
            var validate = await validator.ValidateAsync(timeDto);
            if (!validate.IsValid)
            {
                return BadRequest(validate.Errors.Select(e => e.ErrorMessage));
            }
            var result = await _appointmentService.UpdateAppointmentTime(timeDto);
            return StatusCode(result.StatusCode, result.Body);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteTime(int id)
    {
        try
        {
            var result = await _appointmentService.DeleteAppointmentTime(id);
            return StatusCode(result.StatusCode, result.Body);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
}
