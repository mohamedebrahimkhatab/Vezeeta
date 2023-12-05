using Vezeeta.Core.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Vezeeta.Data.Configurations;

internal class AppointmentConfiguration : BaseEntityConfiguration<Appointment>
{
    public override void Configure(EntityTypeBuilder<Appointment> builder)
    {
        base.Configure(builder);
        builder.Property(e => e.Day).IsRequired();

        builder.Property(e => e.DoctorId).IsRequired();
        builder.HasOne(e => e.Doctor).WithMany(e => e.Appointments).HasForeignKey(e => e.DoctorId).OnDelete(DeleteBehavior.Restrict);
    }
}
