using AutoMapper;
using Vezeeta.Core;
using Vezeeta.Core.Contracts;
using Vezeeta.Core.Contracts.DoctorDtos;
using Vezeeta.Core.Models;
using Vezeeta.Core.Services;

namespace Vezeeta.Services.Local;

public class DoctorService : IDoctorService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DoctorService(IUnitOfWork unitOfWork,
                         IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PaginationResult<AdminGetDoctorDto>> AdminGetAll(int page, int pageSize, string search)
    {
        var totalCount = await _unitOfWork.Doctors.CountAsync();
        var totalPages = totalCount / pageSize;
        if (totalCount % pageSize != 0)
        {
            totalPages++;
        }
        if (page > totalPages)
        {
            page = totalPages;
        }
        var skip = (page - 1) * pageSize;
        var query = await _unitOfWork.Doctors.FindAllWithCriteriaPagenationAndIncludesAsync(e =>
                            (e.ApplicationUser.FirstName + " " + e.ApplicationUser.LastName).Contains(search), skip, pageSize, nameof(Doctor.Specialization), nameof(Doctor.ApplicationUser));
        return new PaginationResult<AdminGetDoctorDto>
        {
            TotalCount = totalCount,
            TotalPages = totalPages,
            CurrentPage = page,
            PageSize = pageSize,
            Data = _mapper.Map<IEnumerable<AdminGetDoctorDto>>(query)
        };
    }

    public async Task<PaginationResult<PatientGetDoctorDto>> PatientGetAll(int page, int pageSize, string search)
    {
        var totalCount = await _unitOfWork.Doctors.CountAsync();
        var totalPages = totalCount / pageSize;
        if (totalCount % pageSize != 0)
        {
            totalPages++;
        }
        if (page > totalPages)
        {
            page = totalPages;
        }
        var skip = (page - 1) * pageSize;
        var query = await _unitOfWork.Doctors.FindAllWithCriteriaPagenationAndIncludesAsync(e =>
                            (e.ApplicationUser.FirstName + " " + e.ApplicationUser.LastName).Contains(search), skip, pageSize, nameof(Doctor.Specialization), nameof(Doctor.ApplicationUser), nameof(Doctor.Appointments),
                                        $"{nameof(Doctor.Appointments)}.{nameof(Appointment.AppointmentTimes)}");
        return new PaginationResult<PatientGetDoctorDto> { 
            TotalCount = totalCount, 
            TotalPages = totalPages, 
            CurrentPage = page, 
            PageSize = pageSize, 
            Data = _mapper.Map<IEnumerable<PatientGetDoctorDto>>(query)
        };
    }

    public async Task<Dictionary<string, IGrouping<string, PatientGetDoctorDto>>> GetAllGroupBySpecialize()
    {
        IEnumerable<Doctor> allDoctors = await _unitOfWork.Doctors.FindAllWithCriteriaAndIncludesAsync(e => true, nameof(Doctor.Specialization), nameof(Doctor.ApplicationUser), nameof(Doctor.Appointments),
                                        $"{nameof(Doctor.Appointments)}.{nameof(Appointment.AppointmentTimes)}");
        IEnumerable<PatientGetDoctorDto> doctorDto = _mapper.Map<IEnumerable<PatientGetDoctorDto>>(allDoctors);
        IEnumerable<IGrouping<string, PatientGetDoctorDto>> groupBy = doctorDto.GroupBy(e => e.Specialize??"");
        Dictionary<string, IGrouping<string, PatientGetDoctorDto>> dict = groupBy.ToDictionary(e => e.Key);
        return dict;
    }

    public async Task<Doctor?> GetById(int id)
    {
        return await _unitOfWork.Doctors.FindWithCriteriaAndIncludesAsync(e => e.Id == id, nameof(Doctor.ApplicationUser), nameof(Doctor.Specialization));
    }

    public async Task<Doctor> Create(Doctor doctor)
    {
        await _unitOfWork.Doctors.AddAsync(doctor);
        await _unitOfWork.CommitAsync();
        return doctor;
    }

    public async Task Update(Doctor doctor)
    {
        _unitOfWork.Doctors.Update(doctor);
        await _unitOfWork.CommitAsync();
    }

    public async Task Delete(Doctor doctor)
    {
        Booking? bookings = await _unitOfWork.Bookings.FindWithCriteriaAndIncludesAsync(e => e.DoctorId == doctor.Id);
        if (bookings != null)
        {
            throw new InvalidOperationException("Can not delete this doctor, doctor has bookings");
        }
        _unitOfWork.Doctors.Delete(doctor);
        await _unitOfWork.CommitAsync();
    }

    public async Task<int> GetDoctorId(int userId)
    {
        var doctor = await _unitOfWork.Doctors.FindWithCriteriaAndIncludesAsync(e => e.ApplicationUserId == userId);
        return doctor.Id;
    }
}
