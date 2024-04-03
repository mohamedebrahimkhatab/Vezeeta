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
using Microsoft.AspNetCore.Identity;
using Vezeeta.Core.Models.Identity;
using Vezeeta.Data.Repositories.Implementation;
using Vezeeta.Services.Utilities.FileService;
using Vezeeta.Core.Consts;


namespace Vezeeta.Services.DomainServices.Services;

public class DoctorService : IDoctorService
{
    private readonly IDoctorRepository _repository;
    private readonly IBaseRepository<Specialization> _specializationRepository;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<ApplicationUser> _userManager;

    public DoctorService(IDoctorRepository repository,
                         IBaseRepository<Specialization> specializationRepository,
                         IMapper mapper,
                         IHttpContextAccessor httpContextAccessor,
                         UserManager<ApplicationUser> userManager)
    {
        _repository = repository;
        _specializationRepository = specializationRepository;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
    }

    public async Task<ServiceResponse> AdminGetAll(DoctorParameters doctorParameters)
    {
        try
        {
            PaginationResponse<Doctor> result = await _repository.SearchDoctorsWithPagination(doctorParameters, nameof(Doctor.Specialization), nameof(Doctor.ApplicationUser));
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
            PaginationResponse<Doctor> result = await _repository.SearchDoctorsWithPagination(doctorParameters, nameof(Doctor.Specialization), nameof(Doctor.ApplicationUser), nameof(Doctor.Appointments), $"{nameof(Doctor.Appointments)}.{nameof(Appointment.AppointmentTimes)}");
            _httpContextAccessor.HttpContext.Response.Headers.Append(nameof(result.Pagination), JsonConvert.SerializeObject(result.Pagination));
            return new(StatusCodes.Status200OK, _mapper.Map<IEnumerable<PatientGetDoctorDto>>(result.Data));
        }
        catch (Exception e)
        {
            return new(StatusCodes.Status500InternalServerError, e.Message);
        }

    }

    public async Task<ServiceResponse> GetById(int id)
    {
        try
        {
            var doctor = await _repository.GetByIdAsync(id, nameof(Doctor.ApplicationUser), nameof(Doctor.Specialization));
            if (doctor == null)
                return new(StatusCodes.Status404NotFound, "This Id is not found");
            return new(StatusCodes.Status200OK, _mapper.Map<GetIdDoctorDto>(doctor));
        }
        catch (Exception e)
        {
            return new(StatusCodes.Status500InternalServerError, e.Message);
        }
    }


    public async Task<ServiceResponse> CreateAsync(CreateDoctorDto doctorDto, string root)
    {
        if (string.IsNullOrEmpty(doctorDto.Email))
            return new(StatusCodes.Status400BadRequest, "This Email is invalid!");

        var user = await _userManager.FindByEmailAsync(doctorDto.Email);
        if (user is not null)
            return new(StatusCodes.Status400BadRequest, "This Email is already registered");

        var specialization = await _specializationRepository.GetByIdAsync(doctorDto.SpecializationId);
        if (specialization is null)
            return new(StatusCodes.Status404NotFound, "This Specialization is not found");

        Doctor doctor = _mapper.Map<Doctor>(doctorDto);

        doctor.SpecializationId = specialization.Id;

        if (doctorDto.Image != null)
            doctor.ApplicationUser.PhotoPath = FileUploader.Upload(doctorDto.Image, root);

        IdentityResult result = await _userManager.CreateAsync(doctor.ApplicationUser, "Doc*1234");

        if(!result.Succeeded)
            return new(StatusCodes.Status500InternalServerError, result.Errors.ToString());

        await _userManager.AddToRoleAsync(doctor.ApplicationUser, UserRoles.Doctor);

        await _repository.AddAsync(doctor);

        //var message = new Message(new string[] { doctorDto.Email }, "Your Pass", "Doc*1234");
        //await _emailSender.SendEmailAsync(message);

        return new(StatusCodes.Status201Created, doctor);
    }

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
