using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vezeeta.Core.Models.Identity;

namespace Vezeeta.Data.Configurations;

internal class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
        builder.Property(e => e.LastName).IsRequired().HasMaxLength(50);
        builder.Property(e => e.Gender).IsRequired();
        builder.Property(e => e.DateOfBirth).IsRequired();

        ApplicationUser user = new()
        {
            Id = 1,
            UserName = "Admin",
            FirstName = "Admin",
            LastName = "Admin",
            Gender = Core.Enums.Gender.Male,
            DateOfBirth = DateTime.Parse("1980-06-12"),
            Email = "admin@gmail.com",
            LockoutEnabled = false,
            PhoneNumber = "1234567890"
        };

        PasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();
        passwordHasher.HashPassword(user, "Admin*123");

        builder.HasData(user);
    }
}
