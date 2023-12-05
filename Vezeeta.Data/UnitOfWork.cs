using Vezeeta.Core;
using Vezeeta.Core.Models;
using Vezeeta.Core.Repositories;
using Vezeeta.Data.Repositories;
using Vezeeta.Core.Models.Identity;

namespace Vezeeta.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public IBaseRepository<Coupon> Coupons { get; private set; }
    public IBaseRepository<Doctor> Doctors { get; private set; }
    public IBaseRepository<Specialization> Specializations { get; private set; }
    public IBaseRepository<ApplicationUser> ApplicationUsers { get; private set; }


    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        Coupons = new BaseRepository<Coupon>(_context);
        Doctors = new BaseRepository<Doctor>(_context);
        Specializations = new BaseRepository<Specialization>(_context);
        ApplicationUsers = new BaseRepository<ApplicationUser>(_context);
    }
    public void Commit() => _context.SaveChanges();

    public Task CommitAsync() => _context.SaveChangesAsync();

    public void Dispose() => _context.Dispose();
}
