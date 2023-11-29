using Vezeeta.Core.Models;
using Vezeeta.Core.Repositories;

namespace Vezeeta.Core;

public interface IUnitOfWork : IDisposable
{
    public IBaseRepository<Patient> Patients { get; }
    void Commit();
    Task CommitAsync();
}
