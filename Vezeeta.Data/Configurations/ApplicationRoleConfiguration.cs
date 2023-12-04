using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vezeeta.Core.Models.Identity;

namespace Vezeeta.Data.Configurations;

internal class ApplicationRoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
{
    public void Configure(EntityTypeBuilder<ApplicationRole> builder)
    {
        builder.HasData(
            new ApplicationRole() { Id = 1, Name = "Admin", NormalizedName = "ADMIN" },
            new ApplicationRole() { Id = 2, Name = "Doctor", NormalizedName = "DOCTOR" },
            new ApplicationRole() { Id = 3, Name = "Patient", NormalizedName = "PATIENT" });
    }
}
