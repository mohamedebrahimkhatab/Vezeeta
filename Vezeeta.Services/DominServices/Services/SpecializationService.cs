using AutoMapper;
using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;
using Vezeeta.Core.Contracts;
using Vezeeta.Core.Models;
using Vezeeta.Data.Parameters;
using Vezeeta.Data.Repositories.Interfaces;
using Vezeeta.Services.DomainServices.Interfaces;
using Vezeeta.Services.Utilities;

namespace Vezeeta.Services.DomainServices.Services;

public class SpecializationService : ISpecializationService
{
    private readonly IBaseRepository<Specialization> _repository;
    private readonly IMapper _mapper;

    public SpecializationService(IBaseRepository<Specialization> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<ServiceResponse> GetAll(SpecializationParameters parameters)
    {
        try
        {
            var result = _mapper.Map<IEnumerable<SpecializationDto>>(await _repository.GetByConditionAsync(getCondition(parameters)));
            return new(StatusCodes.Status200OK, result);
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
            var result = await _repository.GetByConditionAsync(e => e.Id == id);
            if (result == null)
            {
                return new(StatusCodes.Status404NotFound, "This Specialization is not found");
            }
            return new(StatusCodes.Status200OK, _mapper.Map<SpecializationDto>(result));
        }
        catch (Exception e)
        {
            return new(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    public async Task<ServiceResponse> FindByName(SpecializationParameters parameters)
    {
        try
        {
            var result = await _repository.FindByConditionAsync(getCondition(parameters));
            return new(StatusCodes.Status200OK, _mapper.Map<IEnumerable<SpecializationDto>>(result));
        }
        catch (Exception e)
        {
            return new(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    public async Task<ServiceResponse> GetByName(SpecializationParameters parameters)
    {
        try
        {
            var result = await _repository.GetByConditionAsync(getCondition(parameters));
            return new(StatusCodes.Status200OK, _mapper.Map<SpecializationDto>(result));
        }
        catch (Exception e)
        {
            return new(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    private static Expression<Func<Specialization, bool>> getCondition(SpecializationParameters parameters)
    {
        return e => e.Name.ToLower().Contains(parameters.NameParameters.NameQuery.ToLower());
    }
}
