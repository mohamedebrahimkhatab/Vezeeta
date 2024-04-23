using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using Vezeeta.Core.Enums;
using Vezeeta.Data.Repositories.Interfaces;

namespace Vezeeta.Data.Repositories.Implementation;

public class BookingRepositoryExt : IBookingRepositoryExt
{
    private readonly ApplicationDbContext _context;

    public BookingRepositoryExt(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<DateTime>> GetReserved()
    {
        return await _context.Bookings.Where(e => e.BookingStatus.Equals(BookingStatus.Pending)).Select(e => e.AppointmentRealTime).ToListAsync();
    }
}
