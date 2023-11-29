using Vezeeta.Core;
using Vezeeta.Core.Models;
using Vezeeta.Core.Repositories;
using Vezeeta.Data.Repositories;

namespace Vezeeta.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public IBaseRepository<User> Users { get; private set; }
    public IBaseRepository<Doctor> Doctors { get; private set; }
    public IBaseRepository<Specialization> Specializations { get; private set; }

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        Users = new BaseRepository<User>(_context);
        Doctors = new BaseRepository<Doctor>(_context);
        Specializations = new BaseRepository<Specialization>(_context);
    }
    public void Commit() => _context.SaveChanges();

    public Task CommitAsync() => _context.SaveChangesAsync();

    public void Dispose() => _context.Dispose();
}
