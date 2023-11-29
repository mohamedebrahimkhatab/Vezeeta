using Vezeeta.Core.Models;

namespace Vezeeta.Core.Services;

public interface IPatientService
{
    Task<IEnumerable<Patient>> GetAll();
    Task<Patient> GetById(int id);

    Task<Patient> Create(Patient patient);
}
