using Vezeeta.Core.Contracts;
using Vezeeta.Core.Models;

namespace Vezeeta.Core.Services;

public interface ISpecializationService
{
    Task<IEnumerable<SpecializationDto>> GetAll();
    Task<SpecializationDto?> GetById(int id);
    Task<SpecializationDto?> GetByName(string name);
    Task<IEnumerable<SpecializationDto>> FindBySearch(string search);
}
