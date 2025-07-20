
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

    public IEnumerable<T> GetAll()
    {
        return _context.Set<T>().AsNoTracking().Take(10).ToList();
    }

    public T? GetById(Expression<Func<T,bool>>predicate)
    {
        return _context.Set<T>().AsNoTracking().SingleOrDefault(predicate);
    }

    public T Create(T entity)
    {
        if (entity == null)
            throw new Exception(entity+" é null");
        _context.Set<T>().Add(entity);
        _context.SaveChanges();
        return entity;
    }

    public T Update(T entity)
    {
        if (entity == null)
            throw new Exception(entity + " é null");
        _context.Set<T>().Update(entity);
        _context.SaveChanges();
        return entity;
    }

    public T Delete(T entity)
    {
        if (entity == null)
            throw new Exception(entity + " é null");
        _context.Set<T>().Remove(entity);
        _context.SaveChanges();
        return entity;
    }
}

