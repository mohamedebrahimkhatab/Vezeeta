using Vezeeta.Core;
using Vezeeta.Core.Models;
using Vezeeta.Core.Services;

namespace Vezeeta.Services.Local;

public class SpecializationService : ISpecializationService
{
    private readonly IUnitOfWork _unitOfWork;

    public SpecializationService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<IEnumerable<Specialization>> GetAll() 
        => await _unitOfWork.Specializations.GetAllAsync();

    public async Task<Specialization?> GetById(int id) 
        => await _unitOfWork.Specializations.GetByIdAsync(id);

    public async Task<IEnumerable<Specialization>> FindBySearch(string search) 
        => await _unitOfWork.Specializations.FindAllWithCriteriaAndIncludesAsync(e => e.Name.Contains(search));

    public async Task<Specialization> GetByName(string name)
        => await _unitOfWork.Specializations.FindWithCriteriaAndIncludesAsync(e => e.Name == name);
}
