using System.Linq.Expressions;
using APiCursos.Data;
using ApiCursos.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiCursos.Repository;

public class Repository<T> : IRepository<T> where T:class
{
    protected readonly ApiCursoContext _context;

    public Repository(ApiCursoContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _context.Set<T>().AsNoTracking().ToListAsync();
    }

    public async Task<T?> GetByIDAsync(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().AsNoTracking().SingleOrDefaultAsync(predicate);
    }

    public async Task<T> CreateAsync(T entity)
    {
        if (entity is null)
            throw new Exception(entity + " is null");
        await _context.Set<T>().AddAsync(entity);
        return entity;
    }

    public T Update(T entity)
    {
        if (entity is null)
            throw new Exception(entity + " is null");
        _context.Set<T>().Update(entity);
        return entity;
    }

    public T Delete(T entity)
    {
         if (entity is null)
            throw new Exception(entity + " is null"); 
         _context.Set<T>().Remove(entity);
        return entity;
    }
}