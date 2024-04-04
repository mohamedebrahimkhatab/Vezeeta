using Vezeeta.Core.Models;
using Vezeeta.Data.Repositories.Interfaces;
using Vezeeta.Core.Models.Identity;

namespace Vezeeta.Data.Repositories.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    public IBaseRepository<Coupon> Coupons { get; }
    public IBaseRepository<Doctor> Doctors { get; set; }
    public IBaseRepository<Booking> Bookings { get; }
    public IBaseRepository<Appointment> Appointments { get; }
    public IBaseRepository<Specialization> Specializations { get; }
    public IBaseRepository<AppointmentTime> AppointmentTimes { get; }
    public IBaseRepository<ApplicationUser> ApplicationUsers { get; }
    Task BeginTransaction();
    Task CommitTransaction();
    Task RollbackTransaction();
}
