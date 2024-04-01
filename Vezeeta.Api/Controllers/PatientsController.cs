using AutoMapper;
using Vezeeta.Core.Consts;
using Vezeeta.Core.Models;
using Vezeeta.Api.Validators;
using Vezeeta.Core.Contracts;
using Microsoft.AspNetCore.Mvc;
using Vezeeta.Core.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Vezeeta.Core.Contracts.PatientDtos;
using Vezeeta.Core.Contracts.BookingDtos;
using Microsoft.AspNetCore.Authorization;
using Vezeeta.Services.DomainServices.Interfaces;

namespace Vezeeta.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class PatientsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IPatientService _patientService;
    private readonly IWebHostEnvironment _hostingEnvironment;

    public PatientsController(IMapper mapper, UserManager<ApplicationUser> userManager, IPatientService patientService, IWebHostEnvironment hostingEnvironment)
    {
        _mapper = mapper;
        _userManager = userManager;
        _patientService = patientService;
        _hostingEnvironment = hostingEnvironment;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromForm]RegisterPatientDto patientDto)
    {
        try
        {
            var validator = new RegisterPatientDtoValidator();
            var validate = await validator.ValidateAsync(patientDto);
            if (!validate.IsValid)
            {
                return BadRequest(validate.Errors.Select(e => e.ErrorMessage));
            }
            ApplicationUser? user = await _userManager.FindByEmailAsync(patientDto.Email ?? "");
            if (user != null)
            {
                return BadRequest("this email is already taken");
            }

            ApplicationUser patient = _mapper.Map<ApplicationUser>(patientDto);
            if(patientDto.Image is not null )
            {
                patient.PhotoPath = ProcessUploadedFile(patientDto.Image);
            }
            IdentityResult result = await _userManager.CreateAsync(patient, patientDto.Password ?? "");

            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, result.Errors.ToString());
            }

            await _userManager.AddToRoleAsync(patient, UserRoles.Patient);

            return Created();
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    private string ProcessUploadedFile(IFormFile photo)
    {
        string uniqueFileName = "";
        if (photo != null)
        {

            string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
            uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                photo.CopyTo(fileStream);
            }
        }

        return uniqueFileName;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<PaginationResult<GetPatientDto>>> GetAll(int? page, int? pageSize, string? search) 
        => Ok(await _patientService.GetAll(page ?? 1, pageSize ?? 10, search ?? ""));

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById(int id)
    {
        ApplicationUser? patient = await _patientService.GetById(id);
        if(patient == null)
        {
            return NotFound("Patient not found");
        }
        GetPatientDto patientDto = _mapper.Map<GetPatientDto>(patient);
        IEnumerable<Booking> bookings = await _patientService.GetPatientBookings(id);
        return Ok(new GetByIdPatientDto { 
            Details = _mapper.Map<GetPatientDto>(patient),
            Bookings = _mapper.Map<List<PatientGetBookingDto>>(bookings)
        });
    }
}
