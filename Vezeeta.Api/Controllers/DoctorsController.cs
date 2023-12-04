using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Vezeeta.Core.Consts;
using Vezeeta.Core.Contracts.DoctorDtos;
using Vezeeta.Core.Models;
using Vezeeta.Core.Models.Identity;
using Vezeeta.Core.Services;
using Vezeeta.Services.Local;

namespace Vezeeta.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class DoctorsController : ControllerBase
{
    private readonly IDoctorService _doctorService;
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;

    public DoctorsController(IDoctorService doctorService, IMapper mapper, UserManager<ApplicationUser> userManager)
    {
        _doctorService = doctorService;
        _mapper = mapper;
        _userManager = userManager;
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
        var result = await _doctorService.GetById(id);
        if (result == null)
        {
            return NotFound();
        }
        return Ok(_mapper.Map<GetIdDoctorDto>(result));
    }

    [HttpPost]
    public async Task<ActionResult<Doctor>> Add(CreateDoctorDto doctorDto)
    {
        try
        {
            var user = await _userManager.FindByEmailAsync(doctorDto.Email);
            if (user != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "this email is already taken");
            }
            var doctor = _mapper.Map<Doctor>(doctorDto);

            await _userManager.CreateAsync(doctor.ApplicationUser, "Doc*1234");

            await _userManager.AddToRoleAsync(doctor.ApplicationUser, UserRoles.Doctor);

            await _doctorService.Create(doctor);
            return Created();
        }
        catch (Exception e)
        {
            return BadRequest(e.InnerException?.Message);
        }
    }

}
