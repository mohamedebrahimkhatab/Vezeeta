using Microsoft.EntityFrameworkCore;

namespace Vezeeta.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
}
