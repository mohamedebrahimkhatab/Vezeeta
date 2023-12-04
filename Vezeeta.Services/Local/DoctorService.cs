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

    public async Task<IEnumerable<Doctor>> GetAll()
    {
        return await _unitOfWork.Doctors.FindAllWithCriteriaAndIncludesAsync(e => true, nameof(Doctor.Specialization), nameof(Doctor.ApplicationUser));
    }
    public async Task<IEnumerable<Doctor>> GetAllWithSearch(string search)
    {
        return await _unitOfWork.Doctors.FindAllWithCriteriaAndIncludesAsync(e =>
                            e.ApplicationUser.FirstName.Contains(search) || e.ApplicationUser.LastName.Contains(search),
                            nameof(Doctor.Specialization), nameof(Doctor.ApplicationUser));
    }
    public async Task<IEnumerable<Doctor>> GetAllWithPagenation(int page, int pageSize)
    {
        int skip = (page - 1) * pageSize;
        return await _unitOfWork.Doctors.FindAllWithCriteriaPagenationAndIncludesAsync(e => true, skip, pageSize, nameof(Doctor.Specialization), nameof(Doctor.ApplicationUser));
    }

    public async Task<IEnumerable<Doctor>> GetAllWithPagenationAndSearch(int page, int pageSize, string search)
    {
        int skip = (page - 1) * pageSize;
        return await _unitOfWork.Doctors.FindAllWithCriteriaPagenationAndIncludesAsync(e => 
                            (e.ApplicationUser.FirstName.Contains(search) || e.ApplicationUser.LastName.Contains(search)), skip, pageSize, nameof(Doctor.Specialization), nameof(Doctor.ApplicationUser));
    }

    public async Task<Doctor?> GetById(int id)
    {
        return await _unitOfWork.Doctors.FindWithCriteriaAndIncludesAsync(e => e.Id == id, nameof(Doctor.ApplicationUser), nameof(Doctor.Specialization));
    }

    public async Task<Doctor> Create(Doctor doctor)
    {
        await _unitOfWork.Doctors.AddAsync(doctor);
        await _unitOfWork.CommitAsync();
        return doctor;
    }

    public async Task Update(Doctor doctor)
    {
        _unitOfWork.Doctors.Update(doctor);
        await _unitOfWork.CommitAsync();
    }

    public async Task Delete(Doctor doctor)
    {
        _unitOfWork.Doctors.Delete(doctor);
        await _unitOfWork.CommitAsync();
    }
}
