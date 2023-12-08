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
    private readonly IBookingService _bookingService;

    public PatientService(IUnitOfWork unitOfWork, IBookingService bookingService)
    {
        _unitOfWork = unitOfWork;
        _bookingService = bookingService;
    }

    public async Task<IEnumerable<ApplicationUser>> GetAll(int page, int pageSize, string search)
    {
        int skip = (page - 1) * pageSize;
        return await _unitOfWork.ApplicationUsers.FindAllWithCriteriaPagenationAndIncludesAsync(e => 
                                                    e.UserType.Equals(UserType.Patient) && (e.FirstName + " " + e.LastName).Contains(search), skip, pageSize);
    }

    public async Task<ApplicationUser?> GetById(int id)
    {
        return await _unitOfWork.ApplicationUsers.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Booking>> GetPatientBookings(int patientId)
    {
        return await _bookingService.GetPatientBookings(patientId);
    }

}
