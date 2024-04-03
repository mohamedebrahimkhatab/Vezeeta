using Vezeeta.Core.Contracts.DoctorDtos;
using Vezeeta.Core.Contracts;
using Vezeeta.Core.Models;
using Vezeeta.Data.Parameters;
using Vezeeta.Data.Utilities;
using Vezeeta.Services.Utilities;

namespace Vezeeta.Services.DomainServices.Interfaces;

public interface IDoctorService
{
    Task<ServiceResponse> AdminGetAll(DoctorParameters doctorParameters);
    Task<ServiceResponse> PatientGetAll(DoctorParameters doctorParameters);
    Task<ServiceResponse> GetById(int id);
    Task<ServiceResponse> CreateAsync(CreateDoctorDto doctorDto, string root);
    Task<ServiceResponse> UpdateAsync(UpdateDoctorDto doctorDto, string root);
    //Task Delete(Doctor doctor);
    //Task<int> GetDoctorId(int userId);

}
