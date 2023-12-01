using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Vezeeta.Core.Models;
using Vezeeta.Core.Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Vezeeta.Data.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    private readonly ApplicationDbContext _context;

    public BaseRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public T? GetById(int id) => _context.Set<T>().Find(id);

    public async Task<T?> GetByIdAsync(int id) => await _context.Set<T>().FindAsync(id);

    public IEnumerable<T> GetAll() => _context.Set<T>().ToList();

    public async Task<IEnumerable<T>> GetAllAsync() => await _context.Set<T>().ToListAsync();

    private IQueryable<T> IncludesQuery(IQueryable<T> query, params string[] includes)
    {
        if (includes != null)
            foreach (string incluse in includes)
                query = query.Include(incluse);
        return query;
    }

    public T? FindWithCriteriaAndIncludes(Expression<Func<T, bool>> criteria, params string[] includes)
    {
        IQueryable<T> query = _context.Set<T>();

        query = IncludesQuery(query, includes);

        return query.FirstOrDefault(criteria);
    }

    public async Task<T?> FindWithCriteriaAndIncludesAsync(Expression<Func<T, bool>> criteria, params string[] includes)
    {
        IQueryable<T> query = _context.Set<T>();

        query = IncludesQuery(query, includes);

        return await query.FirstOrDefaultAsync(criteria);
    }

    public IEnumerable<T> FindAllWithCriteriaAndIncludes(Expression<Func<T, bool>> criteria, params string[] includes)
    {
        IQueryable<T> query = _context.Set<T>();

        query = IncludesQuery(query, includes);

        return query.Where(criteria).ToList();
    }

    public IEnumerable<T> FindAllWithCriteriaPagenationAndIncludes(Expression<Func<T, bool>> criteria, int skip, int take, params string[] includes)
    {
        IQueryable<T> query = _context.Set<T>();

        query = IncludesQuery(query, includes);

        query = query.Where(criteria);

        return query.Skip(skip).Take(take).ToList();
    }

    public async Task<IEnumerable<T>> FindAllWithCriteriaAndIncludesAsync(Expression<Func<T, bool>> criteria, params string[] includes)
    {
        IQueryable<T> query = _context.Set<T>();

        query = IncludesQuery(query, includes);

        return await query.Where(criteria).ToListAsync();
    }

    public async Task<IEnumerable<T>> FindAllWithCriteriaPagenationAndIncludesAsync(Expression<Func<T, bool>> criteria, int skip, int take, params string[] includes)
    {
        IQueryable<T> query = _context.Set<T>();

        query = IncludesQuery(query, includes);

        return await query.Where(criteria).Skip(skip).Take(take).ToListAsync();
    }

    public T Add(T entity)
    {
        _context.Set<T>().Add(entity);
        return entity;
    }

    public async Task<T> AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        return entity;
    }

    public IEnumerable<T> AddRange(IEnumerable<T> entities)
    {
        _context.Set<T>().AddRange(entities);
        return entities;
    }

    public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
    {
        await _context.Set<T>().AddRangeAsync(entities);
        return entities;
    }

    public T Update(T entity)
    {
        _context.Update(entity);
        return entity;
    }

    public void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

    public void DeleteRange(IEnumerable<T> entities)
    {
        _context.Set<T>().RemoveRange(entities);
    }

    public int Count()
    {
        return _context.Set<T>().Count();
    }

    public int Count(Expression<Func<T, bool>> criteria)
    {
        return _context.Set<T>().Count(criteria);
    }

    public async Task<int> CountAsync()
    {
        return await _context.Set<T>().CountAsync();
    }

    public async Task<int> CountAsync(Expression<Func<T, bool>> criteria)
    {
        return await _context.Set<T>().CountAsync(criteria);
    }
}
