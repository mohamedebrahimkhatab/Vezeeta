using Vezeeta.Core;
using Vezeeta.Core.Models;
using Vezeeta.Data.Repositories;
using Vezeeta.Core.Models.Identity;
using Vezeeta.Data.Repositories.Interfaces;

namespace Vezeeta.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public IBaseRepositoryBig<Coupon> Coupons { get; private set; }
    public IBaseRepositoryBig<Doctor> Doctors { get; private set; }
    public IBaseRepositoryBig<Booking> Bookings { get; private set; }
    public IBaseRepositoryBig<Appointment> Appointments { get; private set; }
    public IBaseRepositoryBig<Specialization> Specializations { get; private set; }
    public IBaseRepositoryBig<ApplicationUser> ApplicationUsers { get; private set; }
    public IBaseRepositoryBig<AppointmentTime> AppointmentTimes { get; private set; }


    public IDoctorRepository Test { get; set; }

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        Coupons = new BaseRepositoryBig<Coupon>(_context);
        Doctors = new BaseRepositoryBig<Doctor>(_context);
        Bookings = new BaseRepositoryBig<Booking>(_context);
        Appointments = new BaseRepositoryBig<Appointment>(_context);
        Specializations = new BaseRepositoryBig<Specialization>(_context);
        AppointmentTimes = new BaseRepositoryBig<AppointmentTime>(_context);
        ApplicationUsers = new BaseRepositoryBig<ApplicationUser>(_context);
        Test = new DoctorRepository(_context);
    }
    public void Commit() => _context.SaveChanges();

    public Task CommitAsync() => _context.SaveChangesAsync();

    public void Dispose() => _context.Dispose();
}
