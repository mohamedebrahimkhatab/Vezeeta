using Microsoft.AspNetCore.Mvc;
using Vezeeta.Services.Interfaces;

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
    public async Task<IActionResult> GetAll() => StatusCode(StatusCodes.Status200OK,await _service.GetAll());

    [HttpGet]
    public async Task<IActionResult> GetById(int id) => Ok(await _service.GetById(id));

    [HttpGet]
    public async Task<IActionResult> GetByName(string name) => Ok(await _service.GetByName(name));

    [HttpGet]
    public async Task<IActionResult> FindBySearch(string search) => Ok(await _service.FindBySearch(search));
}
