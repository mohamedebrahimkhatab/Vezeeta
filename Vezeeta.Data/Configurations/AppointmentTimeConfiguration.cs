using Vezeeta.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Vezeeta.Data.Configurations;

internal class AppointmentTimeConfiguration : BaseEntityConfiguration<AppointmentTime>
{
    public override void Configure(EntityTypeBuilder<AppointmentTime> builder)
    {
        base.Configure(builder);
        builder.Property(e => e.Time).IsRequired();

        builder.Property(e => e.AppointmentId).IsRequired();
        builder.HasOne(e => e.Appointment).WithMany(e => e.AppointmentTimes).HasForeignKey(e => e.AppointmentId).OnDelete(DeleteBehavior.Restrict);
    }
}
