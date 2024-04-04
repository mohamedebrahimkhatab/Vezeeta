using AutoMapper;
using Microsoft.AspNetCore.Http;
using Vezeeta.Core.Contracts.CouponDtos;
using Vezeeta.Core.Models;
using Vezeeta.Data.Repositories.Interfaces;
using Vezeeta.Services.DomainServices.Interfaces;
using Vezeeta.Services.Utilities;

namespace Vezeeta.Services.DomainServices.Services;

public class CouponService : ICouponService
{
    private readonly IMapper _mapper;
    private readonly IBaseRepository<Coupon> _repository;

    public CouponService(IMapper mapper, IBaseRepository<Coupon> repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<ServiceResponse> GetAll()
    {
        try
        {
            var result = await _repository.FindByConditionAsync(e => true);
            return new(StatusCodes.Status200OK, _mapper.Map<IEnumerable<CouponDto>>(result));
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
            return new(StatusCodes.Status201Created, coupon);
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
            //await CheckIfCouponApplied(coupon.DiscountCode);

            _mapper.Map(couponDto, coupon);
            await _repository.UpdateAsync(coupon);
            return new(StatusCodes.Status204NoContent, null);
        }
        catch (Exception e)
        {
            return new(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    //public async Task CheckIfCouponApplied(string? discountCode)
    //{
    //    Booking? booking = await _unitOfWork.Bookings.FindWithCriteriaAndIncludesAsync(e => e.DiscountCode == discountCode);
    //    if (booking != null)
    //    {
    //        throw new InvalidOperationException("this Coupon is applied to booking/s");
    //    }
    //}
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
