using AutoMapper;
using Vezeeta.Core.Consts;
using Vezeeta.Core.Models;
using Vezeeta.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Vezeeta.Core.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Vezeeta.Core.Contracts.DoctorDtos;

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
    public async Task<ActionResult<IEnumerable<GetDoctorDto>>> GetAll()
    {
        IEnumerable<Doctor> result = await _doctorService.GetAll();

        return Ok(_mapper.Map<IEnumerable<GetDoctorDto>>(result));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetDoctorDto>>> GetAllWithSearch(string search)
    {
        IEnumerable<Doctor> result = await _doctorService.GetAllWithSearch(search);
        return Ok(_mapper.Map<IEnumerable<GetDoctorDto>>(result));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetDoctorDto>>> GetAllWithPagenation(int page, int pageSize)
    {
        IEnumerable<Doctor> result = await _doctorService.GetAllWithPagenation(page, pageSize);
        return Ok(_mapper.Map<IEnumerable<GetDoctorDto>>(result));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetDoctorDto>>> GetAllWithPagenationAndSearch(int page, int pageSize, string search)
    {
        IEnumerable<Doctor> result = await _doctorService.GetAllWithPagenationAndSearch(page, pageSize, search);
        return Ok(_mapper.Map<IEnumerable<GetDoctorDto>>(result));
    }

    [HttpGet("{id}")]
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
    public async Task<IActionResult> Add(CreateDoctorDto doctorDto)
    {
        try
        {
            ApplicationUser? user = await _userManager.FindByEmailAsync(doctorDto.Email);
            if (user != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "this email is already taken");
            }

            Specialization specialization = await _specializationService.GetByName(doctorDto.Specialize);

            if (specialization == null)
            {
                return NotFound("Specialization Does not exist");
            }

            Doctor doctor = _mapper.Map<Doctor>(doctorDto);
            doctor.SpecializationId = specialization.Id;

            IdentityResult result = await _userManager.CreateAsync(doctor.ApplicationUser, "Doc*1234");

            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "this email is already taken");
            }

            await _userManager.AddToRoleAsync(doctor.ApplicationUser, UserRoles.Doctor);

            await _doctorService.Create(doctor);
            return Created();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateDoctorDto doctorDto)
    {
        try
        {
            Doctor? doctor = await _doctorService.GetById(doctorDto.Id);
            if (doctor == null)
            {
                throw new Exception("Doctor is Not exist");
            }
            Specialization specialization = await _specializationService.GetByName(doctorDto.Specialize);
            if (specialization == null)
            {
                throw new Exception("Specialize is Not exist");
            }
            doctor.ApplicationUser.FirstName = doctorDto.FirstName;
            doctor.ApplicationUser.LastName = doctorDto.LastName;
            doctor.ApplicationUser.Email = doctorDto.Email;
            doctor.ApplicationUser.PhoneNumber = doctorDto.Phone;
            doctor.ApplicationUser.PhotoPath = doctorDto.PhotoPath;
            doctor.ApplicationUser.Gender = doctorDto.Gender;
            doctor.ApplicationUser.DateOfBirth = doctorDto.DateOfBirth;
            doctor.SpecializationId = specialization.Id;

            await _doctorService.Update(doctor);

            return NoContent();
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        Doctor? doctor = await _doctorService.GetById(id);
        if (doctor == null)
            return NotFound("Doctor is not exist");
        var user = doctor.ApplicationUser;
        await _doctorService.Delete(doctor);
        await _userManager.DeleteAsync(user);
        return NoContent();
    }
}
