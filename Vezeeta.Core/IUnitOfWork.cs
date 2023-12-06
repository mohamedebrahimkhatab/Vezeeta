using Vezeeta.Core.Models;
using Vezeeta.Core.Repositories;
using Vezeeta.Core.Models.Identity;

namespace Vezeeta.Core;

public interface IUnitOfWork : IDisposable
{
    public IBaseRepository<Coupon> Coupons { get; }
    public IBaseRepository<Doctor> Doctors { get; }
    public IBaseRepository<Booking> Bookings { get; }
    public IBaseRepository<Appointment> Appointments { get; }
    public IBaseRepository<Specialization> Specializations { get; }
    public IBaseRepository<AppointmentTime> AppointmentTimes { get; }
    public IBaseRepository<ApplicationUser> ApplicationUsers { get; }
    void Commit();
    Task CommitAsync();
}
