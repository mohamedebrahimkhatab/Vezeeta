using System.Linq;
using Vezeeta.Core;
using Vezeeta.Core.Contracts.DashboardDtos;
using Vezeeta.Core.Contracts.DoctorDtos;
using Vezeeta.Core.Enums;
using Vezeeta.Core.Models;
using Vezeeta.Core.Services;

namespace Vezeeta.Services.Local;

public class DashboardService : IDashboardService
{
    private readonly IUnitOfWork _unitOfWork;

    public DashboardService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> GetNumOfDoctors(SearchBy? search)
    {
        if (search == null)
            return await _unitOfWork.Doctors.CountAsync();
        return await _unitOfWork.Doctors.CountAsync(e => e.CreatedAt >= GetSearchDate(search));
    }

    public async Task<int> GetNumOfPatients(SearchBy? search)
    {
        if (search == null)
            return await _unitOfWork.ApplicationUsers.CountAsync(e => e.UserType.Equals(UserType.Patient));
        return await _unitOfWork.ApplicationUsers.CountAsync(e => e.UserType.Equals(UserType.Patient) && e.CreatedAt >= GetSearchDate(search));
    }

    public async Task<object> GetNumOfRequests(SearchBy? search)
    {
        int NumOfRequests, NumOfPendingRequests, NumOfCompletedRequests, NumOfCancelledRequests;

        if (search == null)
        {
            NumOfRequests = await _unitOfWork.Bookings.CountAsync();
            NumOfPendingRequests = await _unitOfWork.Bookings.CountAsync(e => e.BookingStatus.Equals(BookingStatus.Pending));
            NumOfCompletedRequests = await _unitOfWork.Bookings.CountAsync(e => e.BookingStatus.Equals(BookingStatus.Completed));
            NumOfCancelledRequests = await _unitOfWork.Bookings.CountAsync(e => e.BookingStatus.Equals(BookingStatus.Cancelled));
        }
        else
        {
            NumOfRequests = await _unitOfWork.Bookings.CountAsync(e => e.CreatedAt >= GetSearchDate(search));
            NumOfPendingRequests = await _unitOfWork.Bookings.CountAsync(e => e.BookingStatus.Equals(BookingStatus.Pending) && e.CreatedAt >= GetSearchDate(search));
            NumOfCompletedRequests = await _unitOfWork.Bookings.CountAsync(e => e.BookingStatus.Equals(BookingStatus.Completed) && e.CreatedAt >= GetSearchDate(search));
            NumOfCancelledRequests = await _unitOfWork.Bookings.CountAsync(e => e.BookingStatus.Equals(BookingStatus.Cancelled) && e.CreatedAt >= GetSearchDate(search));

        }
        return new { NumOfRequests, NumOfPendingRequests, NumOfCompletedRequests, NumOfCancelledRequests };
    }

    public async Task<IEnumerable<SpecializtionCountDto>?> GetTop5Speializations()
    {
        var bookings = await _unitOfWork.Bookings.FindAllWithCriteriaAndIncludesAsync(e => true, nameof(Booking.Doctor),
                                                                $"{nameof(Booking.Doctor)}.Specialization");
        var result = bookings.GroupBy(e => e.Doctor.Specialization.Name).Select(e => new SpecializtionCountDto { FullName = e.Key, Num = e.Count() });
        result = result.OrderByDescending(e => e.Num).Take(5);
        return result;
    }
    public async Task<IEnumerable<SimpleDoctorDto>?> GetTop10Doctors()
    {
        var bookings = await _unitOfWork.Bookings.FindAllWithCriteriaAndIncludesAsync(e => true, nameof(Booking.Doctor),
                                                                $"{nameof(Booking.Doctor)}.ApplicationUser",
                                                                $"{nameof(Booking.Doctor)}.Specialization");
        var result = bookings.GroupBy(e => e.Doctor).Select(e => new SimpleDoctorDto
        {
            FullName = e.Key.ApplicationUser.FirstName + " " + e.Key.ApplicationUser.LastName,
            PhotoPath = e.Key.ApplicationUser.PhotoPath,
            Specialize = e.Key.Specialization.Name,
            Num = e.Count()
        });
        result = result.OrderByDescending(e => e.Num).Take(10);
        return result;
    }
    private DateTime GetSearchDate(SearchBy? search)
    {
        if (search.Equals(SearchBy.Last24Hours)) return DateTime.Now.AddDays(-1);
        if (search.Equals(SearchBy.LastWeek)) return DateTime.Now.AddDays(-7);
        if (search.Equals(SearchBy.LastMonth)) return DateTime.Now.AddMonths(-1);
        return DateTime.Now.AddYears(-1);
    }
}
