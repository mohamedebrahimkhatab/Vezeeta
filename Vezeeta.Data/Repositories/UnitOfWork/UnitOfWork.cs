using Vezeeta.Core.Models;
using Vezeeta.Core.Models.Identity;
using Vezeeta.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using Vezeeta.Data.Repositories.Implementation;



namespace Vezeeta.Data.Repositories.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public IBaseRepositoryBig<Coupon> Coupons { get; private set; }
    public IDoctorRepository Doctors { get; set; }
    public IBaseRepositoryBig<Booking> Bookings { get; private set; }
    public IBaseRepositoryBig<Appointment> Appointments { get; private set; }
    public IBaseRepositoryBig<Specialization> Specializations { get; private set; }
    public IBaseRepositoryBig<ApplicationUser> ApplicationUsers { get; private set; }
    public IBaseRepositoryBig<AppointmentTime> AppointmentTimes { get; private set; }




    private IDbContextTransaction _transaction = null!;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        Coupons = new BaseRepositoryBig<Coupon>(_context);
        Bookings = new BaseRepositoryBig<Booking>(_context);
        Appointments = new BaseRepositoryBig<Appointment>(_context);
        Specializations = new BaseRepositoryBig<Specialization>(_context);
        AppointmentTimes = new BaseRepositoryBig<AppointmentTime>(_context);
        ApplicationUsers = new BaseRepositoryBig<ApplicationUser>(_context);
        Doctors = new DoctorRepository(_context);
    }

    public async Task BeginTransaction() => _transaction = await _context.Database.BeginTransactionAsync();

    public async Task CommitTransaction() => await _transaction.CommitAsync();

    public async Task RollbackTransaction() => await _transaction.RollbackAsync();

    public void Dispose() => _context.Dispose();
}
