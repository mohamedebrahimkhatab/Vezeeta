using Vezeeta.Data.Parameters;
using Vezeeta.Services.Utilities;
using Vezeeta.Core.Contracts.DoctorDtos;

namespace Vezeeta.Services.DomainServices.Interfaces;

public interface IDoctorService
{
    Task<ServiceResponse> AdminGetAll(DoctorParameters doctorParameters);
    Task<ServiceResponse> PatientGetAll(DoctorParameters doctorParameters);
    Task<ServiceResponse> GetById(int id);
    Task<ServiceResponse> CreateAsync(CreateDoctorDto doctorDto, string root);
    Task<ServiceResponse> UpdateAsync(UpdateDoctorDto doctorDto, string root);
    Task<ServiceResponse> Delete(int id);
}
