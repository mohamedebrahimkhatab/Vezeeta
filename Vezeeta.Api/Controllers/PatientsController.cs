﻿using Vezeeta.Api.Validators;
using Vezeeta.Data.Parameters;
using Microsoft.AspNetCore.Mvc;
using Vezeeta.Core.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Vezeeta.Core.Contracts.PatientDtos;
using Microsoft.AspNetCore.Authorization;
using Vezeeta.Services.DomainServices.Interfaces;

namespace Vezeeta.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class PatientsController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IPatientService _patientService;
    private readonly IWebHostEnvironment _hostingEnvironment;

    public PatientsController(UserManager<ApplicationUser> userManager, IPatientService patientService, IWebHostEnvironment hostingEnvironment)
    {
        _userManager = userManager;
        _patientService = patientService;
        _hostingEnvironment = hostingEnvironment;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll([FromQuery] PatientParameters patientParameters)
    {
        var result = await _patientService.GetAll(patientParameters);
        return StatusCode(result.StatusCode, result.Body);
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _patientService.GetById(id);
        return StatusCode(result.StatusCode, result.Body);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromForm] RegisterPatientDto patientDto)
    {
        try
        {
            var validator = new RegisterPatientDtoValidator();
            var validate = await validator.ValidateAsync(patientDto);
            if (!validate.IsValid)
            {
                return BadRequest(validate.Errors.Select(e => e.ErrorMessage));
            }
            var result = await _patientService.Register(patientDto, _hostingEnvironment.WebRootPath);

            return StatusCode(result.StatusCode, result.Body);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

}
