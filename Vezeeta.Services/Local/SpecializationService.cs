﻿using AutoMapper;
using Vezeeta.Core;
using Vezeeta.Core.Contracts;
using Vezeeta.Core.Contracts.DoctorDtos;
using Vezeeta.Core.Models;
using Vezeeta.Core.Services;

namespace Vezeeta.Services.Local;

public class SpecializationService : ISpecializationService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SpecializationService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<IEnumerable<SpecializationDto>> GetAll() 
        => _mapper.Map<IEnumerable<SpecializationDto>>(await _unitOfWork.Specializations.GetAllAsync());

    public async Task<SpecializationDto?> GetById(int id) 
        => _mapper.Map<SpecializationDto>(await _unitOfWork.Specializations.GetByIdAsync(id));

    public async Task<IEnumerable<SpecializationDto>> FindBySearch(string search) 
        => _mapper.Map<IEnumerable<SpecializationDto>>(await _unitOfWork.Specializations.FindAllWithCriteriaAndIncludesAsync(e => e.Name.Contains(search)));

    public async Task<SpecializationDto?> GetByName(string name)
        => _mapper.Map<SpecializationDto>(await _unitOfWork.Specializations.FindWithCriteriaAndIncludesAsync(e => e.Name == name));
}
