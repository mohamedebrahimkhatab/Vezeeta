using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vezeeta.Core.Models;
using Vezeeta.Core.Services;
using Vezeeta.Services.Local;

namespace Vezeeta.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorsController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetAll() => Ok(await _doctorService.GetAll());

        [HttpGet]
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
        public async Task<ActionResult<Doctor>> Create(Doctor doctor)
        {
            await _doctorService.Create(doctor);
            return Ok(doctor);
        }

    }
}
