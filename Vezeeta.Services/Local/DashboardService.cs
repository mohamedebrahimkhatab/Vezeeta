using Vezeeta.Core;
using Vezeeta.Core.Enums;
using Vezeeta.Core.Services;

namespace Vezeeta.Services.Local;

public class DashboardService : IDashboardService
{
    private readonly IUnitOfWork _unitOfWork;

    public DashboardService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> GetNumOfDoctors()
    {
        return await _unitOfWork.Doctors.CountAsync();
    }

    public async Task<int> GetNumOfPatients()
    {
        return await _unitOfWork.ApplicationUsers.CountAsync(e => e.UserType.Equals(UserType.Patient));
    }
}
