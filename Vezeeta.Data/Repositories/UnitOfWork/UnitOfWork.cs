using Vezeeta.Core.Models;
using Vezeeta.Core.Models.Identity;
using Vezeeta.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using Vezeeta.Data.Repositories.Implementation;



namespace Vezeeta.Data.Repositories.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public IBaseRepository<Coupon> Coupons { get; private set; }
    public IBaseRepository<Doctor> Doctors { get; set; }
    public IBaseRepository<Booking> Bookings { get; private set; }
    public IBaseRepository<Appointment> Appointments { get; private set; }
    public IBaseRepository<Specialization> Specializations { get; private set; }
    public IBaseRepository<ApplicationUser> ApplicationUsers { get; private set; }
    public IBaseRepository<AppointmentTime> AppointmentTimes { get; private set; }




    private IDbContextTransaction _transaction = null!;

    public UnitOfWork(ApplicationDbContext context,
        IBaseRepository<Coupon> coupons,
        IBaseRepository<Doctor> doctors,
        IBaseRepository<Booking> bookings,
        IBaseRepository<Appointment> appointments,
        IBaseRepository<Specialization> specializations,
        IBaseRepository<ApplicationUser> applicationUsers,
        IBaseRepository<AppointmentTime> appointmentTimes)
    {
        _context = context;
        Coupons = coupons;
        Doctors = doctors;
        Bookings = bookings;
        Appointments = appointments;
        Specializations = specializations;
        AppointmentTimes = appointmentTimes;
        ApplicationUsers = applicationUsers;
    }

    public async Task BeginTransaction() => _transaction = await _context.Database.BeginTransactionAsync();

    public async Task CommitTransaction() => await _transaction.CommitAsync();

    public async Task RollbackTransaction() => await _transaction.RollbackAsync();

    public void Dispose() => _context.Dispose();
}
