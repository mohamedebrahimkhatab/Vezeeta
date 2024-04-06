namespace Vezeeta.Core.Contracts.CouponDtos;

public class GetCouponDto : CouponDto
{
    public int Id { get; set; }
    public string? Active { get; set; }
}
