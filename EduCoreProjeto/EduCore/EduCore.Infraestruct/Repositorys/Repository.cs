using System.Linq.Expressions;
using EduCore.Domain.Entities;
using EduCore.Domain.Interfaces;
using EduCore.Infraestruct.Context;
using Microsoft.EntityFrameworkCore;

namespace EduCore.Infraestruct.Repositorys;

public class Repository<T> : IRepository<T> where T: Entity
{
    public Repository(AppDbContext context)
    {
        _context = context;
    }
    private readonly AppDbContext _context;
    
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _context.Set<T>().AsNoTracking().ToListAsync();
    }

    public async Task<T?> GetByPredicateAsync(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(predicate);
    }

    public virtual void Create(T entity)
    {
        if (entity is null)
            throw new ArgumentNullException(nameof(entity));
        _context.Set<T>().Add(entity);
    }

    public void Update(T entity)
    {
        
        if (entity is null)
            throw new ArgumentNullException(nameof(entity));
        _context.Set<T>().Update(entity);
    }

    public void Delete(T entity)
    {
        
        if (entity is null)
            throw new ArgumentNullException(nameof(entity));
        _context.Set<T>().Remove(entity);
    }
}