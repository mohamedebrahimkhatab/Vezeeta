using Vezeeta.Core;
using Vezeeta.Core.Models;
using Vezeeta.Core.Services;

namespace Vezeeta.Services.Local;

public class DoctorService : IDoctorService
{
    private readonly IUnitOfWork _unitOfWork;

    public DoctorService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Doctor> Create(Doctor doctor)
    {
        await _unitOfWork.Doctors.AddAsync(doctor);
        await _unitOfWork.CommitAsync();
        return doctor;
    }

    public async Task<IEnumerable<Doctor>> GetAll(int page, int pageSize, string search)
    {
        int skip = (page - 1) * pageSize;
        return await _unitOfWork.Doctors.FindAllWithCriteriaPagenationAndIncludesAsync(e => (e.User.FirstName.Contains(search) || e.User.LastName.Contains(search)), skip, pageSize, nameof(Doctor.Specialization), nameof(Doctor.User));
    }

    public async Task<Doctor?> GetById(int id)
    {
        return await _unitOfWork.Doctors.FindWithCriteriaAndIncludesAsync(e => e.Id == id, nameof(Doctor.User), nameof(Doctor.Specialization));
    }
}
