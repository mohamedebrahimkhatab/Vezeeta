using Vezeeta.Core.Enums;

namespace Vezeeta.Core.Contracts.CouponDtos;

public class UpdateCouponDto
{
    public int Id { get; set; }
    public string? DiscountCode { get; set; }
    public int NumOfRequests { get; set; }
    public DiscountType DiscountType { get; set; }
    public decimal Value { get; set; }
}
