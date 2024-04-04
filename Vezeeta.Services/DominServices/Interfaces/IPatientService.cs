using Vezeeta.Core.Contracts.DoctorDtos;
using Vezeeta.Core.Contracts;
using Vezeeta.Core.Models;
using Vezeeta.Core.Models.Identity;
using Vezeeta.Core.Contracts.PatientDtos;
using Vezeeta.Data.Parameters;
using Vezeeta.Services.Utilities;

namespace Vezeeta.Services.DomainServices.Interfaces;

public interface IPatientService
{
    Task<ServiceResponse> GetAll(PatientParameters patientParameters);
    Task<ServiceResponse> GetById(int id);
    Task<ServiceResponse> Register(RegisterPatientDto patientDto, string root);
    //Task<IEnumerable<Booking>> GetPatientBookings(int patientId);
}
