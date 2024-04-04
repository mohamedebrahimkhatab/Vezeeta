using Vezeeta.Core.Models;
using Vezeeta.Data.Repositories.Interfaces;
using Vezeeta.Core.Models.Identity;

namespace Vezeeta.Data.Repositories.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    public IBaseRepositoryBig<Coupon> Coupons { get; }
    public IBaseRepository<Doctor> Doctors { get; set; }
    public IBaseRepositoryBig<Booking> Bookings { get; }
    public IBaseRepositoryBig<Appointment> Appointments { get; }
    public IBaseRepositoryBig<Specialization> Specializations { get; }
    public IBaseRepositoryBig<AppointmentTime> AppointmentTimes { get; }
    public IBaseRepositoryBig<ApplicationUser> ApplicationUsers { get; }
    Task BeginTransaction();
    Task CommitTransaction();
    Task RollbackTransaction();
}
