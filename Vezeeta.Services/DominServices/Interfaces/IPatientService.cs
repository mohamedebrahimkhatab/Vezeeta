using Vezeeta.Data.Parameters;
using Vezeeta.Services.Utilities;
using Vezeeta.Core.Contracts.PatientDtos;

namespace Vezeeta.Services.DomainServices.Interfaces;

public interface IPatientService
{
    Task<ServiceResponse> GetAll(PatientParameters patientParameters);
    Task<ServiceResponse> GetById(int id);
    Task<ServiceResponse> Register(RegisterPatientDto patientDto, string root);
}
