using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vezeeta.Core.Contracts;
using Vezeeta.Core.Models;
using Vezeeta.Core.Services;
using Vezeeta.Services.Local;

namespace Vezeeta.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class DoctorsController : ControllerBase
{
    private readonly IDoctorService _doctorService;
    private readonly IMapper _mapper;

    public DoctorsController(IDoctorService doctorService, IMapper mapper)
    {
        _doctorService = doctorService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Doctor>>> GetAll()
    {
        return Ok(await _doctorService.GetAll());
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Doctor>>> GetAllWithSearch(string search)
    {
        return Ok(await _doctorService.GetAllWithSearch(search));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Doctor>>> GetAllWithPagenation(int page, int pageSize) 
        => Ok(await _doctorService.GetAllWithPagenation(page, pageSize));

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Doctor>>> GetAllWithPagenationAndSearch(int page, int pageSize, string search) 
        => Ok(await _doctorService.GetAllWithPagenationAndSearch(page, pageSize, search));

    [HttpGet("{id}")]
    public async Task<ActionResult<Doctor>> GetById(int id)
    {
        var result = await _doctorService.GetById(id);
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<Doctor>> Add(CreateDoctorDto doctorDto)
    {
        var doctor = _mapper.Map<Doctor>(doctorDto);
        await _doctorService.Create(doctor);
        return Ok(doctor);
    }

}
