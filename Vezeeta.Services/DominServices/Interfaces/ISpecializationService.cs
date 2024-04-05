using Vezeeta.Data.Parameters;
using Vezeeta.Services.Utilities;

namespace Vezeeta.Services.DomainServices.Interfaces;

public interface ISpecializationService
{
    Task<ServiceResponse> GetAll(SpecializationParameters parameters);
    Task<ServiceResponse> GetById(int id);
    Task<ServiceResponse> GetByName(SpecializationParameters parameters);
    Task<ServiceResponse> FindByName(SpecializationParameters parameters);
}
