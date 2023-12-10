using Vezeeta.Core.Models;

namespace Vezeeta.Core.Services;

public interface IDoctorService
{
    Task<IEnumerable<Doctor>> AdminGetAll(int page, int pageSize, string search);
    Task<IEnumerable<Doctor>> PatientGetAll(int page, int pageSize, string search);
    Task<Doctor?> GetById(int id);
    Task<Doctor> Create(Doctor doctor);
    Task Update(Doctor doctor);
    Task Delete(Doctor doctor);
    Task<int> GetDoctorId(int userId);
}
