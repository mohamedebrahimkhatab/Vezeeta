using Vezeeta.Core.Enums;
using Vezeeta.Core.Models;
using Vezeeta.Core.Models.Identity;
using Vezeeta.Core.Contracts.DoctorDtos;
using Vezeeta.Data.Repositories.Interfaces;
using Vezeeta.Core.Contracts.DashboardDtos;
using Vezeeta.Services.DomainServices.Interfaces;

namespace Vezeeta.Services.DomainServices.Services;

public class DashboardService : IDashboardService
{
    private readonly IDashboardRepository _repository;

    public DashboardService(IDashboardRepository repository)
    {
        _repository = repository;
    }

    public async Task<int> GetNumOfDoctors(SearchBy? search)
    {
        if (search == null)
            return await _repository.GetCountAsync<Doctor>(e => true);
        return await _repository.GetCountAsync<Doctor>(e => e.CreatedAt >= GetSearchDate(search));
    }

    public async Task<int> GetNumOfPatients(SearchBy? search)
    {
        if (search == null)
            return await _repository.GetCountAsync<ApplicationUser>(e => e.UserType.Equals(UserType.Patient));
        return await _repository.GetCountAsync<ApplicationUser>(e => e.UserType.Equals(UserType.Patient) && e.CreatedAt >= GetSearchDate(search));
    }

    public async Task<object> GetNumOfRequests(SearchBy? search)
    {
        int NumOfRequests, NumOfPendingRequests, NumOfCompletedRequests, NumOfCancelledRequests;

        if (search == null)
        {
            NumOfRequests = await _repository.GetCountAsync<Booking>(e => true);
            NumOfPendingRequests = await _repository.GetCountAsync<Booking>(e => e.BookingStatus.Equals(BookingStatus.Pending));
            NumOfCompletedRequests = await _repository.GetCountAsync<Booking>(e => e.BookingStatus.Equals(BookingStatus.Completed));
            NumOfCancelledRequests = await _repository.GetCountAsync<Booking>(e => e.BookingStatus.Equals(BookingStatus.Cancelled));
        }
        else
        {
            NumOfRequests = await _repository.GetCountAsync<Booking>(e => e.CreatedAt >= GetSearchDate(search));
            NumOfPendingRequests = await _repository.GetCountAsync<Booking>(e => e.BookingStatus.Equals(BookingStatus.Pending) && e.CreatedAt >= GetSearchDate(search));
            NumOfCompletedRequests = await _repository.GetCountAsync<Booking>(e => e.BookingStatus.Equals(BookingStatus.Completed) && e.CreatedAt >= GetSearchDate(search));
            NumOfCancelledRequests = await _repository.GetCountAsync<Booking>(e => e.BookingStatus.Equals(BookingStatus.Cancelled) && e.CreatedAt >= GetSearchDate(search));

        }
        return new { NumOfRequests, NumOfPendingRequests, NumOfCompletedRequests, NumOfCancelledRequests };
    }

    public async Task<IEnumerable<SpecializtionCountDto>?> GetTop5Speializations()
    {
        return await _repository.GetTop5Speializations();
    }
    public async Task<IEnumerable<SimpleDoctorDto>?> GetTop10Doctors()
    {
        return await _repository.GetTop10Doctors();
    }
    private DateTime GetSearchDate(SearchBy? search)
    {
        if (search == null) return DateTime.MinValue;
        if (search.Equals(SearchBy.Last24Hours)) return DateTime.Now.AddDays(-1);
        if (search.Equals(SearchBy.LastWeek)) return DateTime.Now.AddDays(-7);
        if (search.Equals(SearchBy.LastMonth)) return DateTime.Now.AddMonths(-1);
        return DateTime.Now.AddYears(-1);
    }
}
