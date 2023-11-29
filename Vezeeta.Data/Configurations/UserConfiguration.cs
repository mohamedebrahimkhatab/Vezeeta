using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vezeeta.Core.Models;

namespace Vezeeta.Data.Configurations
{
    public class UserConfiguration : BaseEntityConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);
            builder.Property(p => p.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(p => p.LastName).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Email).IsRequired();
            builder.Property(p => p.Phone).IsRequired().HasMaxLength(20);
            builder.Property(p => p.Gender).IsRequired();
            builder.Property(p => p.DateOfBirth).IsRequired();
        }
    }
}
