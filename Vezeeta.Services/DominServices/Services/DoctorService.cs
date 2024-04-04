using AutoMapper;
using Newtonsoft.Json;
using System.Linq.Expressions;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

using Vezeeta.Core.Models;
using Vezeeta.Core.Consts;
using Vezeeta.Core.Models.Identity;
using Vezeeta.Core.Contracts.DoctorDtos;

using Vezeeta.Data.Utilities;
using Vezeeta.Data.Parameters;
using Vezeeta.Data.Repositories.Interfaces;

using Vezeeta.Services.Utilities;

using Vezeeta.Services.Utilities.FileService;
using Vezeeta.Services.DomainServices.Interfaces;

namespace Vezeeta.Services.DomainServices.Services;

public class DoctorService : IDoctorService
{
    private readonly IPaginationRepository<Doctor> _repository;
    private readonly IBaseRepository<Specialization> _specializationRepository;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<ApplicationUser> _userManager;

    public DoctorService(IPaginationRepository<Doctor> repository,
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
            PaginationResponse<Doctor> result = await _repository.SearchWithPagination(doctorParameters.PaginationParameters, 
                                                              GetDoctorCondition(doctorParameters), nameof(Doctor.Specialization), nameof(Doctor.ApplicationUser));
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
            PaginationResponse<Doctor> result = await _repository.SearchWithPagination(doctorParameters.PaginationParameters, 
                                                        GetDoctorCondition(doctorParameters), nameof(Doctor.Specialization), nameof(Doctor.ApplicationUser),
                                                        nameof(Doctor.Appointments), $"{nameof(Doctor.Appointments)}.{nameof(Appointment.AppointmentTimes)}");
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
            var doctor = await _repository.GetByConditionAsync(e => e.Id == id, nameof(Doctor.ApplicationUser), nameof(Doctor.Specialization));
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
        try
        {
            if (string.IsNullOrEmpty(doctorDto.Email))
                return new(StatusCodes.Status400BadRequest, "This Email is invalid!");

            var user = await _userManager.FindByEmailAsync(doctorDto.Email);
            if (user is not null)
                return new(StatusCodes.Status400BadRequest, "This Email is already registered");

            var specialization = await _specializationRepository.GetByConditionAsync(e => e.Id == doctorDto.SpecializationId);
            if (specialization is null)
                return new(StatusCodes.Status404NotFound, "This Specialization is not found");

            Doctor doctor = _mapper.Map<Doctor>(doctorDto);

            doctor.SpecializationId = specialization.Id;

            if (doctorDto.Image != null)
                doctor.ApplicationUser.PhotoPath = FileUploader.Upload(doctorDto.Image, root);

            IdentityResult result = await _userManager.CreateAsync(doctor.ApplicationUser, "Doc*1234");

            if (!result.Succeeded)
                return new(StatusCodes.Status500InternalServerError, result.Errors.ToString());

            await _userManager.AddToRoleAsync(doctor.ApplicationUser, UserRoles.Doctor);

            await _repository.AddAsync(doctor);

            //var message = new Message(new string[] { doctorDto.Email }, "Your Pass", "Doc*1234");
            //await _emailSender.SendEmailAsync(message);

            return new(StatusCodes.Status201Created, _mapper.Map<GetIdDoctorDto>(doctor));

        }
        catch (Exception e)
        {
            return new(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    public async Task<ServiceResponse> UpdateAsync(UpdateDoctorDto doctorDto, string root)
    {
        try
        {
            var doctor = await _repository.GetByConditionAsync(e => e.Id == doctorDto.DoctorId, nameof(Doctor.Specialization), nameof(Doctor.ApplicationUser));
            if (doctor == null)
            {
                return new(StatusCodes.Status404NotFound, "this doctor is not found");
            }
            doctorDto.PhotoPath = FileUploader.Update(doctorDto.Image, doctorDto.PhotoPath, root);

            _mapper.Map(doctorDto, doctor);

            await _repository.UpdateAsync(doctor);
            return new(StatusCodes.Status204NoContent, _mapper.Map<GetIdDoctorDto>(doctor));
        }
        catch (Exception e)
        {
            return new(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    //public async Task Delete(int id)
    //{
    //    Booking? bookings = await _unitOfWork.Bookings.FindWithCriteriaAndIncludesAsync(e => e.DoctorId == doctor.Id);
    //    if (bookings != null)
    //    {
    //        throw new InvalidOperationException("Can not delete this doctor, doctor has bookings");
    //    }
    //    _unitOfWork.Doctors.Delete(doctor);
    //    await _unitOfWork.Doctors.SaveChanges();
    //}

    Expression<Func<Doctor, bool>> GetDoctorCondition(DoctorParameters doctorParameters)
    {
        return e => (doctorParameters.SpecializeId == 0 || e.SpecializationId == doctorParameters.SpecializeId) &&
                                    (e.ApplicationUser.FirstName + " " + e.ApplicationUser.LastName).ToLower().Contains(doctorParameters.NameParameters.NameQuery.ToLower());

    }
}
