using Vezeeta.Core.Models;
using Vezeeta.Data.Repositories.Interfaces;
using Vezeeta.Core.Models.Identity;

namespace Vezeeta.Data;

public interface IUnitOfWork : IDisposable
{
    public IBaseRepositoryBig<Coupon> Coupons { get; }
    public IBaseRepositoryBig<Doctor> Doctors { get; }
    public IBaseRepositoryBig<Booking> Bookings { get; }
    public IBaseRepositoryBig<Appointment> Appointments { get; }
    public IBaseRepositoryBig<Specialization> Specializations { get; }
    public IBaseRepositoryBig<AppointmentTime> AppointmentTimes { get; }
    public IBaseRepositoryBig<ApplicationUser> ApplicationUsers { get; }
    public IDoctorRepository Test { get; set; }
    void Commit();
    Task CommitAsync();
}
