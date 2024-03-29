using AutoMapper;
using Vezeeta.Core.Models;
using Vezeeta.Core.Contracts.CouponDtos;

namespace Vezeeta.Core.Mapping;

public class CouponProfile :Profile
{
    public CouponProfile()
    {
        CreateMap<CouponDto, Coupon>().ReverseMap();

        CreateMap<UpdateCouponDto, Coupon>();
    }
}
