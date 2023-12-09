using Vezeeta.Core.Contracts.DashboardDtos;
using Vezeeta.Core.Contracts.DoctorDtos;
using Vezeeta.Core.Models;

namespace Vezeeta.Core.Services;

public interface IDashboardService
{
    Task<int> GetNumOfDoctors();
    Task<int> GetNumOfPatients();
    Task<object> GetNumOfRequests();
    Task<IEnumerable<SpecializtionCountDto>?> GetTop5Speializations();
    Task<IEnumerable<SimpleDoctorDto>?> GetTop10Doctors();
}
