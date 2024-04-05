using Vezeeta.Data.Parameters;
using Microsoft.AspNetCore.Mvc;
using Vezeeta.Services.DomainServices.Interfaces;

namespace Vezeeta.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class SpecializationsController : ControllerBase
{
    private readonly ISpecializationService _service;

    public SpecializationsController(ISpecializationService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(SpecializationParameters parameters)
    {
        var result = await _service.GetAll(parameters);
        return StatusCode(result.StatusCode, result.Body);
    }

    [HttpGet]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _service.GetById(id);
        return StatusCode(result.StatusCode, result.Body);
    }

    [HttpGet]
    public async Task<IActionResult> GetByName(SpecializationParameters parameters)
    {
        var result = await _service.GetByName(parameters);
        return StatusCode(result.StatusCode, result.Body);
    }

    [HttpGet]
    public async Task<IActionResult> FindByName(SpecializationParameters parameters)
    {
        var result = await _service.FindByName(parameters);
        return StatusCode(result.StatusCode, result.Body);
    }
}
