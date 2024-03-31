using Vezeeta.Core.Contracts.DoctorDtos;
using Vezeeta.Core.Contracts;
using Vezeeta.Core.Models;
using Vezeeta.Core.Parameters;

namespace Vezeeta.Services.Interfaces;

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

    Task<IEnumerable<Doctor>> TestGetAll(DoctorParameters doctorParameters);
}
