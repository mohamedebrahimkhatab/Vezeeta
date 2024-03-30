using Vezeeta.Core.Contracts.DoctorDtos;
using Vezeeta.Core.Contracts;
using Vezeeta.Core.Models;

namespace Vezeeta.Core.Services;

public interface IDoctorService
{
    Task<PaginationResult<AdminGetDoctorDto>> AdminGetAll(int page, int pageSize, string search);
    Task<PaginationResult<PatientGetDoctorDto>> PatientGetAll(int page, int pageSize, string search);
    Task<IEnumerable<PatientGetDoctorDto>> GetBySpecializeId(int specializeId);
    Task<Doctor?> GetById(int id);
    Task<Doctor> Create(Doctor doctor);
    Task Update(Doctor doctor);
    Task Delete(Doctor doctor);
    Task<int> GetDoctorId(int userId);
}
