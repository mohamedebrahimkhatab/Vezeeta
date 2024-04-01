using System.Linq.Expressions;

namespace Vezeeta.Data.Repositories.Interfaces;

public interface IBaseRepositoryBig<T> where T : class
{
    T? GetById(int id);
    Task<T?> GetByIdAsync(int id);

    IEnumerable<T> GetAll();
    Task<IEnumerable<T>> GetAllAsync();

    T? FindWithCriteriaAndIncludes(Expression<Func<T, bool>> criteria, params string[] includes);
    Task<T?> FindWithCriteriaAndIncludesAsync(Expression<Func<T, bool>> criteria,params string[] includes);

    IEnumerable<T> FindAllWithCriteriaAndIncludes(Expression<Func<T, bool>> criteria, params string[] includes);
    IEnumerable<T> FindAllWithCriteriaPagenationAndIncludes(Expression<Func<T, bool>> criteria, int skip, int take, params string[] includes);


    Task<IEnumerable<T>> FindAllWithCriteriaAndIncludesAsync(Expression<Func<T, bool>> criteria, params string[] includes);
    Task<IEnumerable<T>> FindAllWithCriteriaPagenationAndIncludesAsync(Expression<Func<T, bool>> criteria, int skip, int take, params string[] includes);


    T Add(T entity);
    Task<T> AddAsync(T entity);
    IEnumerable<T> AddRange(IEnumerable<T> entities);
    Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);

    T Update(T entity);

    void Delete(T entity);
    void DeleteRange(IEnumerable<T> entities);

    int Count();
    int Count(Expression<Func<T, bool>> criteria);
    Task<int> CountAsync();
    Task<int> CountAsync(Expression<Func<T, bool>> criteria);

    Task SaveChanges();
}
