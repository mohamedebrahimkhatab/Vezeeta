using AutoMapper;
using Vezeeta.Core.Models;
using Vezeeta.Core.Consts;
using System.Security.Claims;
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

    //[HttpPost]
    //public async Task<IActionResult> Add(DoctorAppointmentDto dto)
    //{
    //    try
    //    {
    //        var validator = new DoctorAppointmentDtoValidator();
    //        var validate = await validator.ValidateAsync(dto);
    //        if (!validate.IsValid)
    //        {
    //            return BadRequest(validate.Errors.Select(e => e.ErrorMessage));
    //        }
    //        int userId = int.Parse(User.FindFirstValue("Id") ?? "");
    //        int doctorId = await _appointmentService.GetDoctorId(userId);
    //        var appointments = _mapper.Map<List<Appointment>>(dto.Days);
    //        await _appointmentService.AddAppointmentsAndPrice(doctorId, dto.Price, appointments);
    //        return Created();

    //    }
    //    catch (Exception e)
    //    {
    //        return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
    //    }
    //}

    //[HttpPut]
    //public async Task<IActionResult> UpdateTime(UpdateTimeDto timeDto)
    //{
    //    try
    //    {
    //        var validator = new UpdateTimeDtoValidator();
    //        var validate = await validator.ValidateAsync(timeDto);
    //        if (!validate.IsValid)
    //        {
    //            return BadRequest(validate.Errors.Select(e => e.ErrorMessage));
    //        }
    //        AppointmentTime? time = await _appointmentService.GetAppointmentTime(timeDto.Id);
    //        if (time == null)
    //        {
    //            return NotFound("Time not Found");
    //        }
    //        int userId = int.Parse(User.FindFirstValue("Id") ?? "");
    //        int doctorId = await _appointmentService.GetDoctorId(userId);
    //        if (time?.Appointment?.DoctorId != doctorId)
    //        {
    //            return Unauthorized("You are unauthorized to change other doctors' appointments");
    //        }
    //        time = _mapper.Map(timeDto, time);
    //        await _appointmentService.UpdateAppointmentTime(time);
    //        return NoContent();
    //    }
    //    catch (Exception e)
    //    {
    //        return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
    //    }
    //}

    //[HttpDelete]
    //public async Task<IActionResult> DeleteTime(int id)
    //{
    //    try
    //    {
    //        AppointmentTime? time = await _appointmentService.GetAppointmentTime(id);
    //        if (time == null)
    //        {
    //            return NotFound("Time not Found");
    //        }
    //        int userId = int.Parse(User.FindFirstValue("Id") ?? "");
    //        int doctorId = await _appointmentService.GetDoctorId(userId);
    //        if (time?.Appointment?.DoctorId != doctorId)
    //        {
    //            return Unauthorized("You are unauthorized to delete other doctors' appointments");
    //        }
    //        await _appointmentService.DeleteAppointmentTime(time);
    //        return NoContent();
    //    }
    //    catch (Exception e)
    //    {
    //        return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
    //    }
    //}
}
