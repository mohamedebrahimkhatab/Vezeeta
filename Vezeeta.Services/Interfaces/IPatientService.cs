using Vezeeta.Core.Contracts.DoctorDtos;
using Vezeeta.Core.Contracts;
using Vezeeta.Core.Models;
using Vezeeta.Core.Models.Identity;
using Vezeeta.Core.Contracts.PatientDtos;

namespace Vezeeta.Services.Interfaces;

public interface IPatientService
{
    Task<PaginationResult<GetPatientDto>> GetAll(int page, int pageSize, string search);
    Task<ApplicationUser?> GetById(int id);
    Task<IEnumerable<Booking>> GetPatientBookings(int patientId);
}
