using Vezeeta.Core.Models.Identity;

namespace Vezeeta.Core.Services;

public interface IPatientService
{
    Task<IEnumerable<ApplicationUser>> GetAll();
    Task<IEnumerable<ApplicationUser>> GetAllWithSearch(string search);
    Task<IEnumerable<ApplicationUser>> GetAllWithPagenation(int page, int pageSize);
    Task<IEnumerable<ApplicationUser>> GetAllWithPagenationAndSearch(int page, int pageSize, string search);
    Task<ApplicationUser?> GetById(int id);
}
