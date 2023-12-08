using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vezeeta.Core.Models;

namespace Vezeeta.Data.Configurations;

internal class BookingsConfiguration :BaseEntityConfiguration<Booking>
{
    public override void Configure(EntityTypeBuilder<Booking> builder)
    {
        base.Configure(builder);
        builder.Property(e => e.BookingStatus).IsRequired();
        builder.Property(e => e.FinalPrice).IsRequired().HasPrecision(5,2);
        
        builder.Property(e => e.DoctorId).IsRequired();
        builder.HasOne(e => e.Doctor).WithMany().HasForeignKey(e => e.DoctorId).OnDelete(DeleteBehavior.Restrict);

        builder.Property(e => e.PatientId).IsRequired();
        builder.HasOne(e => e.Patient).WithMany().HasForeignKey(e => e.PatientId).OnDelete(DeleteBehavior.Restrict);

        builder.Property(e => e.AppointmentTimeId).IsRequired();
        builder.HasOne(e => e.AppointmentTime).WithMany().HasForeignKey(e => e.AppointmentTimeId).OnDelete(DeleteBehavior.Restrict);

    }
}
