using Vezeeta.Core.Enums;
using Microsoft.AspNetCore.Mvc;
using Vezeeta.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Vezeeta.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
[AllowAnonymous]
public class DashboardController : ControllerBase
{
    private readonly IDashboardService _dashboardService;

    public DashboardController(IDashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    [HttpGet]
    public async Task<ActionResult<int>> NumOfDoctors(SearchBy? search) => Ok(await _dashboardService.GetNumOfDoctors(search));

    [HttpGet]
    public async Task<ActionResult<int>> NumOfPatients(SearchBy? search) => Ok(await _dashboardService.GetNumOfPatients(search));

    [HttpGet]
    public async Task<IActionResult> NumOfRequests(SearchBy? search) => Ok(await _dashboardService.GetNumOfRequests(search));

    [HttpGet]
    public async Task<IActionResult> Top5Specializations() => Ok(await _dashboardService.GetTop5Speializations());
    
    [HttpGet]
    public async Task<IActionResult> Top10Doctors() => Ok(await _dashboardService.GetTop10Doctors());

}
