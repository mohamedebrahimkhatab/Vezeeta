using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vezeeta.Core.Services;

namespace Vezeeta.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
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
    }
}
