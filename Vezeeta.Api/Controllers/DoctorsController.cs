﻿using Vezeeta.Core.Consts;
using Vezeeta.Api.Validators;
using Vezeeta.Data.Parameters;
using Microsoft.AspNetCore.Mvc;
using Vezeeta.Services.Utilities;
using Vezeeta.Core.Contracts.DoctorDtos;
using Microsoft.AspNetCore.Authorization;
using Vezeeta.Services.DomainServices.Interfaces;

namespace Vezeeta.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class DoctorsController : ControllerBase
{
    private readonly IDoctorService _doctorService;
    private readonly IWebHostEnvironment _hostingEnvironment;
    //private readonly IEmailSender _emailSender;

    public DoctorsController(IDoctorService doctorService,
                            IWebHostEnvironment hostingEnvironment)
    {
        _doctorService = doctorService;
        _hostingEnvironment = hostingEnvironment;
        //_emailSender = emailSender;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> AdminGetAll([FromQuery] DoctorParameters doctorParameters)
    {
        var result = await _doctorService.AdminGetAll(doctorParameters);
        return StatusCode(result.StatusCode, result.Body);
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> PatientGetAll([FromQuery]DoctorParameters doctorParameters)
    {
        var result = await _doctorService.PatientGetAll(doctorParameters);
        return StatusCode(result.StatusCode, result.Body);
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<ActionResult<GetIdDoctorDto>> GetById(int id)
    {
        ServiceResponse result = await _doctorService.GetById(id);
        return StatusCode(result.StatusCode, result.Body);
    }

    [HttpPost]
    [Authorize(Roles = UserRoles.Admin)]
    public async Task<IActionResult> Add([FromForm] CreateDoctorDto doctorDto)
    {
        try
        {
            var validator = new CreateDoctorDtoValidator();
            var validate = await validator.ValidateAsync(doctorDto);
            if (!validate.IsValid)
            {
                return BadRequest(validate.Errors.Select(e => e.ErrorMessage));
            }
            var result = await _doctorService.CreateAsync(doctorDto, _hostingEnvironment.WebRootPath);
            return StatusCode(result.StatusCode, result.Body);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }



    [HttpPut]
    [Authorize(Roles = UserRoles.Admin)]
    public async Task<IActionResult> Edit([FromForm] UpdateDoctorDto doctorDto)
    {
        try
        {
            var validator = new UpdateDoctorDtoValidator();
            var validate = await validator.ValidateAsync(doctorDto);
            if (!validate.IsValid)
            {
                return BadRequest(validate.Errors.Select(e => e.ErrorMessage));
            }
            
            var result = await _doctorService.UpdateAsync(doctorDto, _hostingEnvironment.WebRootPath);

            return StatusCode(result.StatusCode, result.Body);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = UserRoles.Admin)]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var result = await _doctorService.Delete(id);
            return StatusCode(result.StatusCode, result.Body);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
}
