using Microsoft.EntityFrameworkCore;
using Vezeeta.Core.Models;
using Vezeeta.Data.Configurations;

namespace Vezeeta.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PatientConfiguration());
        modelBuilder.ApplyConfiguration(new SpecializationConfiguration());

    }

    public DbSet<Patient> Patients { get; set; }
    public DbSet<Specialization> Specializations { get; set; }
}
