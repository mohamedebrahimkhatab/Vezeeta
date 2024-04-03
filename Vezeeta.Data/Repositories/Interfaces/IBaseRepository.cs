namespace Vezeeta.Data.Repositories.Interfaces;

public interface IBaseRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int id, params string[] includes);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}
