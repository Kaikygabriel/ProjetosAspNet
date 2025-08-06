
using System.Linq.Expressions;
using CatalogoApi.Data;
using CatalogoApi.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace CatalogoApi.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly CatalogoContext _context;

    public Repository(CatalogoContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _context.Set<T>().AsNoTracking().ToListAsync();
    }

    public async Task<T?> GetByIdAsync(Expression<Func<T,bool>>predicate)
    {
        return await _context.Set<T>().AsNoTracking().SingleOrDefaultAsync(predicate);
    }

    public T Create(T entity)
    {
        if (entity == null)
            throw new Exception(entity+" é null");
        _context.Set<T>().Add(entity);
        return entity;
    }

    public T Update(T entity)
    {
        if (entity == null)
            throw new Exception(entity + " é null");
        _context.Set<T>().Update(entity);
        return entity;
    }

    public T Delete(T entity)
    {
        if (entity == null)
            throw new Exception(entity + " é null");
        _context.Set<T>().Remove(entity);
        return entity;
    }
}

