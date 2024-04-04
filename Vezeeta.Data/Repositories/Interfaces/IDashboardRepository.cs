using System.Linq.Expressions;
using Vezeeta.Core.Contracts.DashboardDtos;
using Vezeeta.Core.Contracts.DoctorDtos;

namespace Vezeeta.Data.Repositories.Interfaces;

public interface IDashboardRepository
{
    Task<int> GetCountAsync<T>(Expression<Func<T, bool>> condition, params string[] includes) where T : class;
    Task<IEnumerable<SpecializtionCountDto>> GetTop5Speializations();
    Task<IEnumerable<SimpleDoctorDto>> GetTop10Doctors();
}
