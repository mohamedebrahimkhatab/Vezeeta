using AutoMapper;
using Vezeeta.Data;
using Vezeeta.Core.Enums;
using Vezeeta.Core.Models;
using Vezeeta.Core.Contracts;
using Vezeeta.Services.Interfaces;
using Vezeeta.Core.Models.Identity;
using Vezeeta.Core.Contracts.PatientDtos;

namespace Vezeeta.Services.ModelServices;

public class PatientService : IPatientService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBookingService _bookingService;

    public PatientService(IUnitOfWork unitOfWork, IBookingService bookingService, IMapper mapper)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _bookingService = bookingService;
    }

    public async Task<PaginationResult<GetPatientDto>> GetAll(int page, int pageSize, string search)
    {
        var totalCount = await _unitOfWork.ApplicationUsers.CountAsync(e => e.UserType.Equals(UserType.Patient) && (e.FirstName + " " + e.LastName).Contains(search));
        var totalPages = totalCount / pageSize;
        if (totalCount % pageSize != 0)
        {
            totalPages++;
        }
        if (page > totalPages)
        {
            page = totalPages;
        }
        int skip = (page - 1) * pageSize;
        var query =  await _unitOfWork.ApplicationUsers.FindAllWithCriteriaPagenationAndIncludesAsync(e => 
                                                    e.UserType.Equals(UserType.Patient) && (e.FirstName + " " + e.LastName).Contains(search), skip, pageSize);

        return new PaginationResult<GetPatientDto>
        {
            TotalCount = totalCount,
            TotalPages = totalPages,
            CurrentPage = page,
            PageSize = pageSize,
            Data = _mapper.Map<IEnumerable<GetPatientDto>>(query)
        };
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
