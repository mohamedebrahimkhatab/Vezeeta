﻿using System.Linq.Expressions;
using Vezeeta.Core.Models;

namespace Vezeeta.Core.Repositories;

public interface IBaseRepository<T> where T : BaseEntity
{
    T GetById(int id);
    Task<T> GetByIdAsync(int id);

    IEnumerable<T> GetAll();
    Task<IEnumerable<T>> GetAllAsync();

    T Find(Expression<Func<T, bool>> criteria, string[] includes = null);
    Task<T> FindAsync(Expression<Func<T, bool>> criteria, string[] includes = null);

    IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, string[] includes = null);
    IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, int take, int skip);


    Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, string[] includes = null);
    Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, int skip, int take);


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
}
