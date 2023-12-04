using Vezeeta.Core.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Vezeeta.Data.Configurations;

internal class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(e => e.Gender).IsRequired();
        builder.Property(e => e.UserType).IsRequired();
        builder.Property(e => e.DateOfBirth).IsRequired();
        builder.Property(e => e.LastName).IsRequired().HasMaxLength(50);
        builder.Property(e => e.FirstName).IsRequired().HasMaxLength(50);

        ApplicationUser user = new()
        {
            Id = 1,
            UserName = "admin@vezeeta.com",
            NormalizedUserName = "ADMIN@VEZEETA.COM",
            Email = "admin@vezeeta.com",
            NormalizedEmail = "ADMIN@VEZEETA.COM",
            FirstName = "Admin",
            LastName = "Admin",
            Gender = Core.Enums.Gender.Male,
            UserType = Core.Enums.UserType.Admin,
            DateOfBirth = DateTime.Parse("1980-06-12"),
            LockoutEnabled = false,
            PhoneNumber = "1234567890"
        };

        PasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();
        user.PasswordHash = passwordHasher.HashPassword(user, "Admin*123");
        builder.HasData(user);
    }
}
