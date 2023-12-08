namespace Vezeeta.Core.Services;

public interface IDashboardService
{
    Task<int> GetNumOfDoctors();
    Task<int> GetNumOfPatients();

}
