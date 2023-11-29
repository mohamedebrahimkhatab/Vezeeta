using Vezeeta.Core;
using Vezeeta.Core.Models;
using Vezeeta.Core.Services;

namespace Vezeeta.Services.Local;

public class PatientService : IPatientService
{
    private readonly IUnitOfWork _unitOfWork;

    public PatientService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Patient> Create(Patient patient)
    {
        await _unitOfWork.Patients.AddAsync(patient);
        await _unitOfWork.CommitAsync();
        return patient;
    }

    public async Task<IEnumerable<Patient>> GetAll() => await _unitOfWork.Patients.GetAllAsync();

    public async Task<Patient> GetById(int id) => await _unitOfWork.Patients.GetByIdAsync(id);


}
