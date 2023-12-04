using Microsoft.AspNetCore.Identity;
using Vezeeta.Core;
using Vezeeta.Core.Enums;
using Vezeeta.Core.Models;
using Vezeeta.Core.Models.Identity;
using Vezeeta.Core.Services;

namespace Vezeeta.Services.Local;

public class PatientService : IPatientService
{
    private readonly IUnitOfWork _unitOfWork;

    public PatientService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<ApplicationUser>> GetAll()
    {
        return await _unitOfWork.ApplicationUsers.FindAllWithCriteriaAndIncludesAsync(e => e.UserType.Equals(UserType.Patient));
    }

    public async Task<IEnumerable<ApplicationUser>> GetAllWithSearch(string search)
    {
        return await _unitOfWork.ApplicationUsers.FindAllWithCriteriaAndIncludesAsync(e => 
                                                   e.UserType.Equals(UserType.Patient) && (e.FirstName + " " + e.LastName).Contains(search));
    }

    public async Task<IEnumerable<ApplicationUser>> GetAllWithPagenation(int page, int pageSize)
    {
        int skip = (page - 1) * pageSize;
        return await _unitOfWork.ApplicationUsers.FindAllWithCriteriaPagenationAndIncludesAsync(e => e.UserType.Equals(UserType.Patient), skip, pageSize);
    }

    public async Task<IEnumerable<ApplicationUser>> GetAllWithPagenationAndSearch(int page, int pageSize, string search)
    {
        int skip = (page - 1) * pageSize;
        return await _unitOfWork.ApplicationUsers.FindAllWithCriteriaPagenationAndIncludesAsync(e => 
                                                    e.UserType.Equals(UserType.Patient) && (e.FirstName + " " + e.LastName).Contains(search), skip, pageSize);
    }

    public async Task<ApplicationUser?> GetById(int id)
    {
        return await _unitOfWork.ApplicationUsers.GetByIdAsync(id);
    }

}
