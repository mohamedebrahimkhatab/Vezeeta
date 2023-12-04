using AutoMapper;
using Vezeeta.Core.Consts;
using Microsoft.AspNetCore.Mvc;
using Vezeeta.Core.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Vezeeta.Core.Contracts.PatientDtos;
using Vezeeta.Core.Services;

namespace Vezeeta.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class PatientsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IPatientService _patientService;

    public PatientsController(IMapper mapper, UserManager<ApplicationUser> userManager, IPatientService patientService)
    {
        _mapper = mapper;
        _userManager = userManager;
        _patientService = patientService;
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterPatientDto patientDto)
    {
        try
        {
            ApplicationUser? user = await _userManager.FindByEmailAsync(patientDto.Email);
            if (user != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "this email is already taken");
            }

            ApplicationUser patient = _mapper.Map<ApplicationUser>(patientDto);

            IdentityResult result = await _userManager.CreateAsync(patient, patientDto.Password);

            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, result.ToString());
            }

            await _userManager.AddToRoleAsync(patient, UserRoles.Patient);

            return Created();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetPatientDto>>> GetAll()
    {
        IEnumerable<ApplicationUser> result = await _patientService.GetAll();

        return Ok(_mapper.Map<IEnumerable<GetPatientDto>>(result));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetPatientDto>>> GetAllWithSearch(string search)
    {
        IEnumerable<ApplicationUser> result = await _patientService.GetAllWithSearch(search);
        return Ok(_mapper.Map<IEnumerable<GetPatientDto>>(result));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetPatientDto>>> GetAllWithPagenation(int page, int pageSize)
    {
        IEnumerable<ApplicationUser> result = await _patientService.GetAllWithPagenation(page, pageSize);
        return Ok(_mapper.Map<IEnumerable<GetPatientDto>>(result));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetPatientDto>>> GetAllWithPagenationAndSearch(int page, int pageSize, string search)
    {
        IEnumerable<ApplicationUser> result = await _patientService.GetAllWithPagenationAndSearch(page, pageSize, search);
        return Ok(_mapper.Map<IEnumerable<GetPatientDto>>(result));
    }
}
