using Vezeeta.Core.Models;

namespace Vezeeta.Core.Services;

public interface ICouponService
{
    Task<IEnumerable<Coupon>> GetAll();
    Task<Coupon?> GetById(int id);
    Task<Coupon?> GetByDiscountCode(string discountCode);
    Task<Coupon> Create(Coupon coupon);
    Task Update(Coupon coupon);
    Task Delete(Coupon coupon);
    Task Deactivate(Coupon coupon);
    Task CheckIfCouponApplied(string? discountCode);
}
