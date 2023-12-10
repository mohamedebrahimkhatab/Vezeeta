using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vezeeta.Core.Consts;
using Vezeeta.Core.Services;

namespace Vezeeta.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
[Authorize(Roles = UserRoles.Admin)]
public class DashboardController : ControllerBase
{
    private readonly IDashboardService _dashboardService;

    public DashboardController(IDashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    [HttpGet]
    public async Task<ActionResult<int>> NumOfDoctors() => Ok(await _dashboardService.GetNumOfDoctors());

    [HttpGet]
    public async Task<ActionResult<int>> NumOfPatients() => Ok(await _dashboardService.GetNumOfPatients());

    [HttpGet]
    public async Task<IActionResult> NumOfRequests() => Ok(await _dashboardService.GetNumOfRequests());

    [HttpGet]
    public async Task<IActionResult> Top5Specializations() => Ok(await _dashboardService.GetTop5Speializations());
    
    [HttpGet]
    public async Task<IActionResult> Top10Doctors() => Ok(await _dashboardService.GetTop10Doctors());

}
