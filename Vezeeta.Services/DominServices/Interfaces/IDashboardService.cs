using Vezeeta.Core.Contracts.DashboardDtos;
using Vezeeta.Core.Contracts.DoctorDtos;
using Vezeeta.Core.Enums;
using Vezeeta.Core.Models;

namespace Vezeeta.Services.DomainServices.Interfaces;

public interface IDashboardService
{
    Task<int> GetNumOfDoctors(SearchBy? search);
    Task<int> GetNumOfPatients(SearchBy? search);
    Task<object> GetNumOfRequests(SearchBy? search);
    Task<IEnumerable<SpecializtionCountDto>?> GetTop5Speializations();
    Task<IEnumerable<SimpleDoctorDto>?> GetTop10Doctors();
}
