using System.Linq.Expressions;
using ApiClientes.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiClientes.Repository.Interfaces;

public class Repository<T> : IRepository<T> where T: class
{
    private readonly ClienteContext _context;

    public Repository(ClienteContext context)
    {
        _context = context;
    }

    public IEnumerable<T> GetAll()
    {
        return _context.Set<T>().AsNoTracking().Take(10).ToList();
    }

    public T? GetById(Expression<Func<T, bool>> predicate)
    {
        return  _context.Set<T>().AsNoTracking().SingleOrDefault(predicate);
    }

    public T Create(T entity)
    {
        if (entity is null)
            throw new Exception($"Error, {entity} is null");
        _context.Set<T>().Add(entity);
        _context.SaveChanges();
        return entity;
    }

    public T Update(T entity)
    {
        if (entity is null)
            throw new Exception($"Error, {entity} is null");
        _context.Set<T>().Update(entity);
        _context.SaveChanges();
        return entity;
    }

    public T Delete(T entity)
    {
        if (entity is null)
            throw new Exception($"Error, {entity} is null");
        _context.Set<T>().Remove(entity);
        _context.SaveChanges();
        return entity;
    }
}