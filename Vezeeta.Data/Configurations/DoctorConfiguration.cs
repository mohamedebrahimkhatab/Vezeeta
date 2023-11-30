using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vezeeta.Core.Models;

namespace Vezeeta.Data.Configurations;

public class DoctorConfiguration : BaseEntityConfiguration<Doctor>
{
    public override void Configure(EntityTypeBuilder<Doctor> builder)
    {
        base.Configure(builder);
        builder.Property(e => e.Price).HasPrecision(5,2);

        builder.Property(e => e.UserId).IsRequired();
        builder.HasOne(e => e.User).WithOne().OnDelete(DeleteBehavior.Restrict);

        builder.Property(e => e.SpecializationId).IsRequired();
        builder.HasOne(e => e.Specialization).WithMany().HasForeignKey(e=>e.SpecializationId).OnDelete(DeleteBehavior.Restrict);
        
    }
}
