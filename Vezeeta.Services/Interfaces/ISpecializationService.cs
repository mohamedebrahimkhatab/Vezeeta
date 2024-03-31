using Vezeeta.Core.Contracts;

namespace Vezeeta.Services.Interfaces;

public interface ISpecializationService
{
    Task<IEnumerable<SpecializationDto>> GetAll();
    Task<SpecializationDto?> GetById(int id);
    Task<SpecializationDto?> GetByName(string name);
    Task<IEnumerable<SpecializationDto>> FindBySearch(string search);
}
