using System.Linq.Expressions;
using Vezeeta.Core.Models;

namespace Vezeeta.Data.Repositories.Interfaces;

public interface IBaseRepository<T> where T : class
{
    Task<IEnumerable<T>> FindByCondition(Expression<Func<T, bool>> condition, params string[] includes);
    Task<T?> GetByConditionAsync(Expression<Func<T, bool>> condition, params string[] includes);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}
