using AutoMapper;
using Vezeeta.Core.Consts;
using Vezeeta.Core.Models;
using Vezeeta.Api.Validators;
using Vezeeta.Core.Contracts;
using Vezeeta.Data.Parameters;
using Microsoft.AspNetCore.Mvc;
using Vezeeta.Core.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Vezeeta.Core.Contracts.DoctorDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Storage.Json;
using System.Text.Json.Serialization;
using System.Net.Http.Json;
using Newtonsoft.Json;
using Vezeeta.Services.DomainServices.Interfaces;
using Vezeeta.Services.Utilities;

namespace Vezeeta.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class DoctorsController : ControllerBase
{
    private readonly IDoctorService _doctorService;
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ISpecializationService _specializationService;
    private readonly IWebHostEnvironment _hostingEnvironment;
    //private readonly IEmailSender _emailSender;

    public DoctorsController(IDoctorService doctorService,
                            IMapper mapper,
                            UserManager<ApplicationUser> userManager,
                            ISpecializationService specializationService,
                            IWebHostEnvironment hostingEnvironment)
    {
        _doctorService = doctorService;
        _mapper = mapper;
        _userManager = userManager;
        _specializationService = specializationService;
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
            await _doctorService.CreateAsync(doctorDto, _hostingEnvironment.WebRootPath);
            return Created();
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    

    //[HttpPut]
    //[Authorize(Roles = UserRoles.Admin)]
    //public async Task<IActionResult> Edit([FromForm] UpdateDoctorDto doctorDto)
    //{
    //    try
    //    {
    //        var validator = new UpdateDoctorDtoValidator();
    //        var validate = await validator.ValidateAsync(doctorDto);
    //        if (!validate.IsValid)
    //        {
    //            return BadRequest(validate.Errors.Select(e => e.ErrorMessage));
    //        }
    //        Doctor? doctor = await _doctorService.GetById(doctorDto.DoctorId);
    //        if (doctor == null)
    //        {
    //            return NotFound("this doctor is not found");
    //        }

    //        if (doctorDto.Image != null)
    //        {
    //            if (doctorDto.PhotoPath != null)
    //            {
    //                string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "images", doctorDto.PhotoPath);
    //                System.IO.File.Delete(filePath);
    //            }
    //            doctorDto.PhotoPath = ProcessUploadedFile(doctorDto.Image);
    //        }

    //        _mapper.Map(doctorDto, doctor);

    //        await _doctorService.Update(doctor);

    //        return NoContent();
    //    }
    //    catch (Exception e)
    //    {
    //        return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
    //    }
    //}

    //[HttpDelete("{id}")]
    //[Authorize(Roles = UserRoles.Admin)]
    //public async Task<IActionResult> Delete(int id)
    //{
    //    try
    //    {
    //        Doctor? doctor = await _doctorService.GetById(id);
    //        if (doctor == null)
    //            return NotFound("Doctor is not exist");
    //        var user = doctor.ApplicationUser;
    //        await _doctorService.Delete(doctor);
    //        await _userManager.DeleteAsync(user);
    //        return NoContent();
    //    }
    //    catch (Exception e)
    //    {
    //        return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
    //    }
    //}
}
