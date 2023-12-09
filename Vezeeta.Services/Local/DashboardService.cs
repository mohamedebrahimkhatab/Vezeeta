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

    public async Task<int> GetNumOfDoctors()
    {
        return await _unitOfWork.Doctors.CountAsync();
    }

    public async Task<int> GetNumOfPatients()
    {
        return await _unitOfWork.ApplicationUsers.CountAsync(e => e.UserType.Equals(UserType.Patient));
    }

    public async Task<object> GetNumOfRequests()
    {
        var NumOfRequests = await _unitOfWork.Bookings.CountAsync();
        var NumOfPendingRequests = await _unitOfWork.Bookings.CountAsync(e => e.BookingStatus.Equals(BookingStatus.Pending));
        var NumOfCompletedRequests = await _unitOfWork.Bookings.CountAsync(e => e.BookingStatus.Equals(BookingStatus.Completed));
        var NumOfCancelledRequests = await _unitOfWork.Bookings.CountAsync(e => e.BookingStatus.Equals(BookingStatus.Cancelled));
        return new {NumOfRequests, NumOfPendingRequests, NumOfCompletedRequests, NumOfCancelledRequests};
    }

    public async Task<IEnumerable<SpecializtionCountDto>?> GetTop5Speializations()
    {
        var bookings = await _unitOfWork.Bookings.FindAllWithCriteriaAndIncludesAsync(e => true, nameof(Booking.Doctor), 
                                                                $"{nameof(Booking.Doctor)}.Specialization");
        var result = bookings.GroupBy(e => e.Doctor.Specialization.Name).Select(e => new SpecializtionCountDto{ FullName = e.Key, Num = e.Count() });
        result = result.OrderByDescending(e => e.Num).Take(5);
        return result;
    }
    public async Task<IEnumerable<SimpleDoctorDto>?> GetTop10Doctors()
    {
        var bookings = await _unitOfWork.Bookings.FindAllWithCriteriaAndIncludesAsync(e => true, nameof(Booking.Doctor),
                                                                $"{nameof(Booking.Doctor)}.ApplicationUser",
                                                                $"{nameof(Booking.Doctor)}.Specialization");
        var result = bookings.GroupBy(e => e.Doctor).Select(e => new SimpleDoctorDto { FullName = e.Key.ApplicationUser.FirstName + " " + e.Key.ApplicationUser.LastName,
                                                                PhotoPath = e.Key.ApplicationUser.PhotoPath,
                                                                Specialize = e.Key.Specialization.Name,Num = e.Count() });
        result = result.OrderByDescending(e => e.Num).Take(10);
        return result;
    }
}
