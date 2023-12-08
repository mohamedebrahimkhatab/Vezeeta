using Vezeeta.Core.Models;
using Vezeeta.Core.Models.Identity;

namespace Vezeeta.Core.Services;

public interface IPatientService
{
    Task<IEnumerable<ApplicationUser>> GetAll(int page, int pageSize, string search);
    Task<ApplicationUser?> GetById(int id);
    Task<IEnumerable<Booking>> GetPatientBookings(int patientId);
}
