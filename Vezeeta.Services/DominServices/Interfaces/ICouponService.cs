using Vezeeta.Services.Utilities;
using Vezeeta.Core.Contracts.CouponDtos;

namespace Vezeeta.Services.DomainServices.Interfaces;

public interface ICouponService
{
    Task<ServiceResponse> GetAll();
    Task<ServiceResponse> Create(CouponDto couponDto);
    Task<ServiceResponse> Update(UpdateCouponDto couponDto);
    Task<ServiceResponse> Deactivate(int id);
    Task<ServiceResponse> Delete(int id);
}
