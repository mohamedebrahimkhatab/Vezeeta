﻿using AutoMapper;
using Newtonsoft.Json;
using Vezeeta.Core.Enums;
using Vezeeta.Core.Consts;
using Vezeeta.Data.Utilities;
using System.Linq.Expressions;
using Vezeeta.Data.Parameters;
using Microsoft.AspNetCore.Http;
using Vezeeta.Services.Utilities;
using Vezeeta.Core.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Vezeeta.Core.Contracts.PatientDtos;
using Vezeeta.Data.Repositories.Interfaces;
using Vezeeta.Services.Utilities.FileService;
using Vezeeta.Services.DomainServices.Interfaces;
using Vezeeta.Core.Models;
using Vezeeta.Core.Contracts.BookingDtos;
using Vezeeta.Core.Contracts.DoctorDtos;

namespace Vezeeta.Services.DomainServices.Services;

public class PatientService : IPatientService
{
    private readonly IMapper _mapper;
    private readonly IPaginationRepository<ApplicationUser> _repository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IBaseRepository<Booking> _bookings;
    private readonly UserManager<ApplicationUser> _userManager;

    public PatientService(IMapper mapper,
                            IPaginationRepository<ApplicationUser> repository,
                            IHttpContextAccessor httpContextAccessor, IBaseRepository<Booking> bookings, UserManager<ApplicationUser> userManager)
    {
        _mapper = mapper;
        _repository = repository;
        _httpContextAccessor = httpContextAccessor;
        _bookings = bookings;
        _userManager = userManager;
    }


    public async Task<ServiceResponse> GetAll(PatientParameters patientParameters)
    {
        try
        {
            PaginationResponse<ApplicationUser> result = await _repository.SearchWithPagination(patientParameters, GetPatientCondition(patientParameters));
            //_httpContextAccessor.HttpContext.Response.Headers.Append(nameof(result.Pagination), JsonConvert.SerializeObject(result.Pagination));
            return new ServiceResponse(StatusCodes.Status200OK, new { result.Pagination, Data = _mapper.Map<IEnumerable<GetPatientDto>>(result.Data) });
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
            var patient = await _repository.GetByConditionAsync(e => e.Id == id && e.UserType.Equals(UserType.Patient));
            if (patient == null)
                return new(StatusCodes.Status404NotFound, "This Id is not found");

            var bookings = await _bookings.FindByConditionAsync(e => e.PatientId == id, nameof(Booking.Doctor), $"{nameof(Booking.Doctor)}.{nameof(Doctor.Specialization)}",
                                                            $"{nameof(Booking.Doctor)}.{nameof(Doctor.ApplicationUser)}",
                                                            nameof(Booking.AppointmentTime), $"{nameof(Booking.AppointmentTime)}.{nameof(AppointmentTime.Appointment)}");

            var dto = new GetByIdPatientDto
            {
                Details = _mapper.Map<GetPatientDto>(patient),
                Bookings = _mapper.Map<IEnumerable<PatientGetBookingDto>>(bookings)
            };


            return new(StatusCodes.Status200OK, dto);
        }
        catch (Exception e)
        {
            return new(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    public async Task<ServiceResponse> Register(RegisterPatientDto patientDto, string root)
    {
        try
        {
            if (string.IsNullOrEmpty(patientDto.Email))
                return new(StatusCodes.Status400BadRequest, "This Email is invalid!");

            ApplicationUser? user = await _userManager.FindByEmailAsync(patientDto.Email);
            if (user is not null)
                return new(StatusCodes.Status400BadRequest, "this email is already taken");

            ApplicationUser patient = _mapper.Map<ApplicationUser>(patientDto);

            if (patientDto.Image is not null)
                patient.PhotoPath = FileUploader.Upload(patientDto.Image, root);

            if (string.IsNullOrEmpty(patientDto.Password))
                return new(StatusCodes.Status400BadRequest, "This Password is invalid!");

            IdentityResult result = await _userManager.CreateAsync(patient, patientDto.Password);

            if (!result.Succeeded)
                return new(StatusCodes.Status500InternalServerError, result.Errors.ToString());

            await _userManager.AddToRoleAsync(patient, UserRoles.Patient);

            return new(StatusCodes.Status201Created, _mapper.Map<GetPatientDto>(patient));
        }
        catch (Exception e)
        {
            return new(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    private Expression<Func<ApplicationUser, bool>> GetPatientCondition(PatientParameters patientParameters)
    {
        return e => e.UserType.Equals(UserType.Patient) && (e.FirstName + " " + e.LastName).ToLower().Contains(patientParameters.NameQuery.ToLower());
    }
}
