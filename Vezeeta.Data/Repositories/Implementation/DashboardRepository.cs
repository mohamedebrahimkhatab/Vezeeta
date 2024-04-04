using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Security.Cryptography;
using Vezeeta.Core.Contracts.DashboardDtos;
using Vezeeta.Core.Contracts.DoctorDtos;
using Vezeeta.Core.Models;
using Vezeeta.Core.Models.Identity;
using Vezeeta.Data.Repositories.Interfaces;

namespace Vezeeta.Data.Repositories.Implementation;

public class DashboardRepository : IDashboardRepository
{
    private readonly ApplicationDbContext _context;

    public DashboardRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> GetCountAsync<T>(Expression<Func<T, bool>> condition, params string[] includes) where T : class
    {
        var query = _context.Set<T>().AsQueryable();
        foreach(string include in includes)
        {
            query = query.Include(include);
        }
        return await query.CountAsync(condition);
    }

    public async Task<IEnumerable<SpecializtionCountDto>> GetTop5Speializations()
    {
        DbSet<Doctor> doctors = _context.Set<Doctor>();
        DbSet<Booking> bookings = _context.Set<Booking>();
        DbSet<Specialization> specializations = _context.Set<Specialization>();

        var query = from doctor in doctors
                     join booking in bookings
                     on doctor.Id equals booking.DoctorId
                     join spec in specializations
                     on doctor.SpecializationId equals spec.Id
                     select new { spec.Name, booking.Id };
        var result = query.GroupBy(x => x.Name).Select(e => new SpecializtionCountDto { FullName = e.Key, Num = e.Count()});
        return await result.OrderByDescending(e => e.Num).Take(5).ToListAsync();
    }

    public async Task<IEnumerable<SimpleDoctorDto>> GetTop10Doctors()
    {
        DbSet<Doctor> doctors = _context.Set<Doctor>();
        DbSet<Booking> bookings = _context.Set<Booking>();
        DbSet<Specialization> specializations = _context.Set<Specialization>();
        DbSet<ApplicationUser> users = _context.Set<ApplicationUser>();
        
        var query = from booking in bookings
                     join doctor in doctors
                     on booking.DoctorId equals doctor.Id
                     join user in users
                     on doctor.ApplicationUserId equals user.Id
                     join specialization in specializations
                     on doctor.SpecializationId equals specialization.Id
                    select new {doctor, booking.Id};

        var result = query.GroupBy(e => e.doctor).Select(e => new SimpleDoctorDto
        {
            FullName = e.Key.ApplicationUser.FirstName + " " + e.Key.ApplicationUser.LastName,
            PhotoPath = e.Key.ApplicationUser.PhotoPath,
            Specialize = e.Key.Specialization.Name,
            Num = e.Count()
        });
        return await result.OrderByDescending(e => e.Num).ToListAsync();
    }
}
