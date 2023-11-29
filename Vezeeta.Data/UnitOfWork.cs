using Vezeeta.Core;
using Vezeeta.Core.Models;
using Vezeeta.Core.Repositories;
using Vezeeta.Data.Repositories;

namespace Vezeeta.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public IBaseRepository<Patient> Patients { get; private set; }

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        Patients = new BaseRepository<Patient>(_context);
    }
    public void Commit() => _context.SaveChanges();

    public Task CommitAsync() => _context.SaveChangesAsync();

    public void Dispose() => _context.Dispose();
}
