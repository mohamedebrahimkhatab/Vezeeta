using AutoMapper;
using Vezeeta.Core.Models;
using Vezeeta.Core.Contracts;
using Vezeeta.Data.Parameters;
using Vezeeta.Core.Contracts.DoctorDtos;
using Vezeeta.Services.DomainServices.Interfaces;
using Vezeeta.Data.Repositories.UnitOfWork;
using Vezeeta.Data.Utilities;
using Vezeeta.Services.Utilities;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Vezeeta.Data.Repositories.Interfaces;


namespace Vezeeta.Services.DomainServices.Services;

public class DoctorService : IDoctorService
{
    private readonly IDoctorRepository _repository;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public DoctorService(IDoctorRepository repository,
                         IMapper mapper,
                         IHttpContextAccessor httpContextAccessor)
    {
        _repository = repository;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ServiceResponse> AdminGetAll(DoctorParameters doctorParameters)
    {
        try
        {
            PaginationResponse<Doctor> result = await _repository.GetAllDoctorWithPagination(doctorParameters);
            _httpContextAccessor.HttpContext.Response.Headers.Append(nameof(result.Pagination), JsonConvert.SerializeObject(result.Pagination));

            return new ServiceResponse(StatusCodes.Status200OK, _mapper.Map<IEnumerable<AdminGetDoctorDto>>(result.Data));
        }
        catch (Exception e)
        {
            return new(StatusCodes.Status500InternalServerError, e.Message);
        }

    }

    public async Task<ServiceResponse> PatientGetAll(DoctorParameters doctorParameters)
    {
        try
        {
            PaginationResponse<Doctor> result = await _repository.GetAllDoctorWithPagination(doctorParameters);
            _httpContextAccessor.HttpContext.Response.Headers.Append(nameof(result.Pagination), JsonConvert.SerializeObject(result.Pagination));
            return new(StatusCodes.Status200OK, _mapper.Map<IEnumerable<PatientGetDoctorDto>>(result.Data));
        }
        catch (Exception e)
        {
            return new(StatusCodes.Status500InternalServerError, e.Message);
        }

    }

    //public async Task<IEnumerable<PatientGetDoctorDto>> GetBySpecializeId(int specializeId)
    //{
    //    IEnumerable<Doctor> allDoctors = await _unitOfWork.Doctors.FindAllWithCriteriaAndIncludesAsync(e => e.SpecializationId == specializeId, nameof(Doctor.Specialization), nameof(Doctor.ApplicationUser), nameof(Doctor.Appointments),
    //                                    $"{nameof(Doctor.Appointments)}.{nameof(Appointment.AppointmentTimes)}");
    //    return _mapper.Map<IEnumerable<PatientGetDoctorDto>>(allDoctors);
    //}

    //public async Task<Doctor?> GetById(int id)
    //{
    //    return await _unitOfWork.Doctors.FindWithCriteriaAndIncludesAsync(e => e.Id == id, nameof(Doctor.ApplicationUser), nameof(Doctor.Specialization));
    //}

    //public async Task<Doctor> Create(Doctor doctor)
    //{
    //    await _unitOfWork.Doctors.AddAsync(doctor);
    //    await _unitOfWork.Doctors.SaveChanges();
    //    return doctor;
    //}

    //public async Task Update(Doctor doctor)
    //{
    //    _unitOfWork.Doctors.Update(doctor);
    //    await _unitOfWork.Doctors.SaveChanges();
    //}

    //public async Task Delete(Doctor doctor)
    //{
    //    Booking? bookings = await _unitOfWork.Bookings.FindWithCriteriaAndIncludesAsync(e => e.DoctorId == doctor.Id);
    //    if (bookings != null)
    //    {
    //        throw new InvalidOperationException("Can not delete this doctor, doctor has bookings");
    //    }
    //    _unitOfWork.Doctors.Delete(doctor);
    //    await _unitOfWork.Doctors.SaveChanges();
    //}

    //public async Task<int> GetDoctorId(int userId)
    //{
    //    var doctor = await _unitOfWork.Doctors.FindWithCriteriaAndIncludesAsync(e => e.ApplicationUserId == userId) ?? new();
    //    return doctor.Id;
    //}
}
