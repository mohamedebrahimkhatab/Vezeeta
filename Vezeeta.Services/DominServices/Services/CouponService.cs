﻿using Vezeeta.Core.Models;
using Vezeeta.Data.Repositories.UnitOfWork;
using Vezeeta.Services.DomainServices.Interfaces;

namespace Vezeeta.Services.DomainServices.Services;

public class CouponService : ICouponService
{
    private readonly IUnitOfWork _unitOfWork;

    public CouponService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Coupon>> GetAll()
    {
        return await _unitOfWork.Coupons.GetAllAsync();
    }

    public async Task<Coupon?> GetById(int id)
    {
        return await _unitOfWork.Coupons.FindWithCriteriaAndIncludesAsync(e => e.Id == id && e.Active);
    }

    public async Task<Coupon?> GetByDiscountCode(string discountCode)
    {
        return await _unitOfWork.Coupons.FindWithCriteriaAndIncludesAsync(e => e.DiscountCode == discountCode);
    }
    public async Task<Coupon> Create(Coupon coupon)
    {
        var checkCoupon = await GetByDiscountCode(coupon.DiscountCode);
        if (checkCoupon != null)
        {
            throw new InvalidOperationException("this code already exist");
        }
        coupon.Active = true;
        await _unitOfWork.Coupons.AddAsync(coupon);
        await _unitOfWork.Coupons.SaveChanges();
        return coupon;
    }
    public async Task Update(Coupon coupon)
    {

        _unitOfWork.Coupons.Update(coupon);
        await _unitOfWork.Coupons.SaveChanges();
    }
    public async Task Delete(Coupon coupon)
    {
        _unitOfWork.Coupons.Delete(coupon);
        await _unitOfWork.Coupons.SaveChanges();
    }

    public async Task Deactivate(Coupon coupon)
    {
        if (!coupon.Active)
            throw new InvalidOperationException("this coupon is already deactivated");
        coupon.Active = false;
        await _unitOfWork.Coupons.SaveChanges();
    }

    public async Task CheckIfCouponApplied(string? discountCode)
    {
        Booking? booking = await _unitOfWork.Bookings.FindWithCriteriaAndIncludesAsync(e => e.DiscountCode == discountCode);
        if (booking != null)
        {
            throw new InvalidOperationException("this Coupon is applied to booking/s");
        }
    }
}