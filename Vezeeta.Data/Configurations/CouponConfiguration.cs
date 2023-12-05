using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vezeeta.Core.Models;

namespace Vezeeta.Data.Configurations;

internal class CouponConfiguration : BaseEntityConfiguration<Coupon>
{
    public override void Configure(EntityTypeBuilder<Coupon> builder)
    {
        base.Configure(builder);
        builder.Property(e => e.DiscountCode).IsRequired();
        builder.Property(e => e.NumOfRequests).IsRequired();
        builder.Property(e => e.Active).HasDefaultValue(true);
        builder.Property(e => e.DiscountType).IsRequired();
        builder.Property(e => e.Value).IsRequired().HasPrecision(5, 2);
    }
}
