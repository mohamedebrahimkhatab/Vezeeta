using AutoMapper;
using Vezeeta.Core.Models;
using Microsoft.AspNetCore.Http;
using Vezeeta.Services.Utilities;
using Vezeeta.Core.Contracts.CouponDtos;
using Vezeeta.Data.Repositories.Interfaces;
using Vezeeta.Services.DomainServices.Interfaces;

namespace Vezeeta.Services.DomainServices.Services;

public class CouponService : ICouponService
{
    private readonly IMapper _mapper;
    private readonly IBaseRepository<Coupon> _repository;
    private readonly IBaseRepository<Booking> _bookings;

    public CouponService(IMapper mapper, IBaseRepository<Coupon> repository, IBaseRepository<Booking> Bookings)
    {
        _mapper = mapper;
        _repository = repository;
        _bookings = Bookings;
    }

    public async Task<ServiceResponse> GetAll()
    {
        try
        {
            var result = await _repository.FindByConditionAsync(e => true);
            return new(StatusCodes.Status200OK, _mapper.Map<IEnumerable<GetCouponDto>>(result));
        }
        catch (Exception e)
        {
            return new(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    public async Task<ServiceResponse> Create(CouponDto couponDto)
    {
        try
        {
            Coupon coupon = _mapper.Map<Coupon>(couponDto);

            var checkCoupon = await GetByDiscountCode(coupon.DiscountCode);
            if (checkCoupon != null)
                return new(StatusCodes.Status400BadRequest, "this code already exist");

            coupon.Active = true;
            await _repository.AddAsync(coupon);
            return new(StatusCodes.Status201Created, _mapper.Map<GetCouponDto>(coupon));
        }
        catch (Exception e)
        {
            return new(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    private async Task<Coupon?> GetByDiscountCode(string discountCode)
    {
        return await _repository.GetByConditionAsync(e => e.DiscountCode == discountCode);
    }
    public async Task<ServiceResponse> Update(UpdateCouponDto couponDto)
    {

        try
        {
            Coupon? coupon = await _repository.GetByConditionAsync(e => e.Id == couponDto.Id);
            if (coupon == null)
            {
                return new(StatusCodes.Status404NotFound, "This coupon not found");
            }

            var booking = await _bookings.GetByConditionAsync(e => e.DiscountCode ==  couponDto.DiscountCode);

            if (booking != null)
                return new(StatusCodes.Status406NotAcceptable, "This Coupon is applied to booking/s");

            _mapper.Map(couponDto, coupon);
            await _repository.UpdateAsync(coupon);
            return new(StatusCodes.Status204NoContent, null);
        }
        catch (Exception e)
        {
            return new(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    public async Task<ServiceResponse> Deactivate(int id)
    {
        try
        {
            Coupon? coupon = await _repository.GetByConditionAsync(e => e.Id == id && e.Active);
            if (coupon == null)
            {
                return new(StatusCodes.Status404NotFound, $"there is no activated coupon with id: {id}");
            }

            coupon.Active = false;
            await _repository.UpdateAsync(coupon);
            return new(StatusCodes.Status204NoContent, null);
        }
        catch (Exception e)
        {
            return new(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    public async Task<ServiceResponse> Delete(int id)
    {
        try
        {
            Coupon? coupon = await _repository.GetByConditionAsync(e => e.Id == id);
            if (coupon == null)
            {
                return new(StatusCodes.Status404NotFound, "This coupon is not found");
            }
            await _repository.DeleteAsync(coupon);
            return new(StatusCodes.Status204NoContent, null);
        }
        catch (Exception e)
        {
            return new(StatusCodes.Status500InternalServerError, e.Message);
        }
    }


}
