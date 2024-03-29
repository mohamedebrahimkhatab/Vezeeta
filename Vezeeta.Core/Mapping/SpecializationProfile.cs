using AutoMapper;
using Vezeeta.Core.Models;
using Vezeeta.Core.Contracts;

namespace Vezeeta.Core.Mapping;

public class SpecializationProfile : Profile
{
    public SpecializationProfile()
    {
        CreateMap<Specialization, SpecializationDto>();
    }
}
