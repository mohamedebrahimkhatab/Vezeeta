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
    //Task<IEnumerable<PatientGetDoctorDto>> GetBySpecializeId(int specializeId);
    //Task<Doctor?> GetById(int id);
    //Task<Doctor> Create(Doctor doctor);
    //Task Update(Doctor doctor);
    //Task Delete(Doctor doctor);
    //Task<int> GetDoctorId(int userId);

}
