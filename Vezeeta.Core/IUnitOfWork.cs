namespace Vezeeta.Core;

public interface IUnitOfWork : IDisposable
{
    void Commit();
    Task CommitAsync();
}
