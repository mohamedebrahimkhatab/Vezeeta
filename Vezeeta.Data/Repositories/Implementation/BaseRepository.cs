using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Vezeeta.Core.Models;
using Vezeeta.Data.Repositories.Interfaces;

namespace Vezeeta.Data.Repositories.Implementation;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    protected readonly ApplicationDbContext _context;

    public BaseRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<T?> GetByConditionAsync(Expression<Func<T, bool>> condition, params string[] includes)
    {
        var query = ApplyIncludes(GetAll(), includes);
        return await ApplyCondition(query, condition).FirstOrDefaultAsync();
    }

    public async Task AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        _context.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        _context.Remove(entity);
        await _context.SaveChangesAsync();
    }

    protected IQueryable<T> GetAll()
    {
        return _context.Set<T>().AsQueryable();
    }

    protected IQueryable<T> ApplyIncludes(IQueryable<T> query,params string[] includes)
    {
        foreach (string include in includes)
        {
            query = query.Include(include);
        }
        return query;
    }

    protected IQueryable<T> ApplyCondition(IQueryable<T> query, Expression<Func<T, bool>> condition)
    {
        return query.Where(condition);
    }
}
