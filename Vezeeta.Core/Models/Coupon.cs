using Vezeeta.Core.Enums;

namespace Vezeeta.Core.Models;

public class Coupon : BaseEntity
{
    public string DiscountCode { get; set; } = null!;
    public int NumOfRequests { get; set; }
    public DiscountType DiscountType { get; set; }
    public decimal Value { get; set; }
    public bool Active { get; set; }
}
 