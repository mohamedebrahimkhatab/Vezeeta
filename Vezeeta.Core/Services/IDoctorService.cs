using Vezeeta.Core.Models;

namespace Vezeeta.Core.Services;

public interface IDoctorService
{
    Task<IEnumerable<Doctor>> GetAll(int page, int pageSize, string search);
    Task<Doctor?> GetById(int id);
    Task<Doctor> Create(Doctor doctor);
}
