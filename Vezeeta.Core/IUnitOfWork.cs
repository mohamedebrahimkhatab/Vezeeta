using Vezeeta.Core.Models;
using Vezeeta.Core.Repositories;

namespace Vezeeta.Core;

public interface IUnitOfWork : IDisposable
{
    public IBaseRepository<Patient> Patients { get; }
    public IBaseRepository<Specialization> Specializations { get; }
    void Commit();
    Task CommitAsync();
}
