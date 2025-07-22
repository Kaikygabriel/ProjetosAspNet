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
    public IEnumerable<T> GetAll()
    {
        return _context.Set<T>().AsNoTracking().Take(10).ToList();
    }

    public T? GetByID(Expression<Func<T, bool>> predicate)
    {
        return _context.Set<T>().AsNoTracking().SingleOrDefault(predicate);
    }

    public T Create(T entity)
    {
        if (entity is null)
            throw new Exception(entity + " is null");
        _context.Set<T>().Add(entity);
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