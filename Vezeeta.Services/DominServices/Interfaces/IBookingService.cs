using Vezeeta.Data.Parameters;
using Vezeeta.Services.Utilities;
using Vezeeta.Core.Contracts.BookingDtos;

namespace Vezeeta.Services.DomainServices.Interfaces;

public interface IBookingService
{
    Task<ServiceResponse> GetPatientBookings();
    Task<ServiceResponse> GetDoctorBookings(BookingParameters parameters);
    Task<ServiceResponse> Book(BookBookingDto bookingDto);
    Task<ServiceResponse> ConfirmCheckUp(int id);
    Task<ServiceResponse> Cancel(int id);
}
