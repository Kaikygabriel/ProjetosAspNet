using System.Data;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ProductsApi.Domain.BackOffice.Interfaces;
using ProductsApi.Infraestruct.Data.Context;

namespace ProductsApi.Infraestruct.Repositorys;

public class Repository<T> : IRepository<T> 
    where T : class, IAggregateRoot
{
    private readonly AppDbContext _context;

    public Repository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        return await _context.Set<T>().AsNoTracking().ToListAsync();
    }

    public async Task<T?> GetByPredicate(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(predicate);
    }

    public void Create(T entity)
    {
        if (entity is null)
            throw new NoNullAllowedException();
        _context.Set<T>().Add(entity);
    }

    public void Update(T entity)
    {
        if (entity is null)
            throw new NoNullAllowedException();
        _context.Set<T>().Update(entity);
    }

    public void Delete(T entity)
    {
        if (entity is null)
            throw new NoNullAllowedException();
        _context.Set<T>().Remove(entity);
    }
}