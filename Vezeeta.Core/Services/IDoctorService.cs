using Vezeeta.Core.Models;

namespace Vezeeta.Core.Services;

public interface IDoctorService
{
    Task<IEnumerable<Doctor>> GetAll();
    Task<IEnumerable<Doctor>> GetAllWithSearch(string search);
    Task<IEnumerable<Doctor>> GetAllWithPagenation(int page, int pageSize);
    Task<IEnumerable<Doctor>> GetAllWithPagenationAndSearch(int page, int pageSize, string search);
    Task<Doctor?> GetById(int id);
    Task<Doctor> Create(Doctor doctor);
}
