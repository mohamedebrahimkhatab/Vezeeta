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
            new ApplicationRole() { Id = 1, Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin" },
            new ApplicationRole() { Id = 2, Name = "Doctor", ConcurrencyStamp = "2", NormalizedName = "Doctor" },
            new ApplicationRole() { Id = 3, Name = "Patient", ConcurrencyStamp = "3", NormalizedName = "Patient" });
    }
}
