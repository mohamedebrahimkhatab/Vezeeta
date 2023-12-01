using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Vezeeta.Core.Contracts.DoctorDtos;
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
        var doctor = _mapper.Map<Doctor>(doctorDto);
        await _doctorService.Create(doctor);
        return Created();
    }

}
