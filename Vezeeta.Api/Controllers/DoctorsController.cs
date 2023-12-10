using AutoMapper;
using Vezeeta.Core.Consts;
using Vezeeta.Core.Models;
using Vezeeta.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Vezeeta.Core.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Vezeeta.Core.Contracts.DoctorDtos;
using Microsoft.AspNetCore.Authorization;
using Vezeeta.Api.Validators;
using Vezeeta.Core.Contracts.CouponDtos;
using FluentValidation;

namespace Vezeeta.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class DoctorsController : ControllerBase
{
    private readonly IDoctorService _doctorService;
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ISpecializationService _specializationService;

    public DoctorsController(IDoctorService doctorService,
                            IMapper mapper,
                            UserManager<ApplicationUser> userManager,
                            ISpecializationService specializationService)
    {
        _doctorService = doctorService;
        _mapper = mapper;
        _userManager = userManager;
        _specializationService = specializationService;
    }

    [HttpGet]
    [Authorize(Roles = UserRoles.Admin)]
    public async Task<ActionResult<IEnumerable<AdminGetDoctorDto>>> AdminGetAll(int? page, int? pageSize, string? search)
    {

        IEnumerable<Doctor> result = await _doctorService.AdminGetAll(page ?? 1, pageSize ?? 10, search ?? "");
        return Ok(_mapper.Map<IEnumerable<AdminGetDoctorDto>>(result));
    }

    [HttpGet]
    [Authorize(Roles = UserRoles.Patient)]
    public async Task<ActionResult<IEnumerable<AdminGetDoctorDto>>> PatientGetAll(int? page, int? pageSize, string? search)
    {

        IEnumerable<Doctor> result = await _doctorService.PatientGetAll(page ?? 1, pageSize ?? 10, search ?? "");
        return Ok(_mapper.Map<IEnumerable<PatientGetDoctorDto>>(result));
    }

    [HttpGet("{id}")]
    [Authorize(Roles = UserRoles.Admin)]
    public async Task<ActionResult<GetIdDoctorDto>> GetById(int id)
    {
        Doctor? result = await _doctorService.GetById(id);
        if (result == null)
        {
            return NotFound();
        }
        return Ok(_mapper.Map<GetIdDoctorDto>(result));
    }

    [HttpPost]
    [Authorize(Roles = UserRoles.Admin)]
    public async Task<IActionResult> Add(CreateDoctorDto doctorDto)
    {
        try
        {
            var validator = new CreateDoctorDtoValidator();
            var validate = await validator.ValidateAsync(doctorDto);
            if (!validate.IsValid)
            {
                return BadRequest(validate.Errors.Select(e => e.ErrorMessage));
            }
            ApplicationUser? user = await _userManager.FindByEmailAsync(doctorDto.Email);
            if (user != null)
            {
                return BadRequest("this email is already taken");
            }

            Specialization specialization = await _specializationService.GetById(doctorDto.SpecializationId);

            if (specialization == null)
            {
                return NotFound("Specialization Does not exist");
            }

            Doctor doctor = _mapper.Map<Doctor>(doctorDto);
            doctor.SpecializationId = specialization.Id;

            IdentityResult result = await _userManager.CreateAsync(doctor.ApplicationUser, "Doc*1234");

            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, result.Errors.ToString());
            }

            await _userManager.AddToRoleAsync(doctor.ApplicationUser, UserRoles.Doctor);

            await _doctorService.Create(doctor);
            return Created();
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpPut]
    [Authorize(Roles = UserRoles.Admin)]
    public async Task<IActionResult> Edit(UpdateDoctorDto doctorDto)
    {
        try
        {
            var validator = new UpdateDoctorDtoValidator();
            var validate = await validator.ValidateAsync(doctorDto);
            if (!validate.IsValid)
            {
                return BadRequest(validate.Errors.Select(e => e.ErrorMessage));
            }
            Doctor? doctor = await _doctorService.GetById(doctorDto.DoctorId);
            if (doctor == null)
            {
                return NotFound("this doctor is not found");
            }

            _mapper.Map(doctorDto, doctor);

            await _doctorService.Update(doctor);

            return NoContent();
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
            Doctor? doctor = await _doctorService.GetById(id);
            if (doctor == null)
                return NotFound("Doctor is not exist");
            var user = doctor.ApplicationUser;
            await _doctorService.Delete(doctor);
            await _userManager.DeleteAsync(user);
            return NoContent();
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
}
