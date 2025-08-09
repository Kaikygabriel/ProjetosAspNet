using System.Linq.Expressions;
using ApiCompras.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace  ApiCompras.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly  VendaContext _context;
    public Repository(VendaContext context)
    {
        _context = context;
    }
    public void Create(T entity)
    {
        if (entity is null)
            throw new ArgumentNullException(nameof(entity));
        _context.Set<T>().Add(entity);
    }

    public void Delete(T entity)
    {
        if (entity is null)
            throw new ArgumentNullException(nameof(entity));
        _context.Set<T>().Remove(entity);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _context.Set<T>().AsNoTracking().ToListAsync();
    }

    public async Task<T?> GetByIdAsync(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().SingleOrDefaultAsync(predicate);
    }

    public void Update(T entity)
    {
        if (entity is null)
            throw new ArgumentNullException(nameof(entity));
        _context.Set<T>().Update(entity);
    }
}