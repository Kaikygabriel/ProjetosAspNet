using System.Linq.Expressions;
using FilmesApi.Data;
using FilmesApi.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FilmesApi.Repository;

public class Repository<T>: IRepository<T> where T:class
{
    protected readonly AppDbContext context;

    public Repository(AppDbContext context)
    {
        this.context = context;
    }

    public IEnumerable<T> GetAll()
    {
        return context.Set<T>().AsNoTracking().ToList();
    }

    public T? GetById(Expression<Func<T, bool>> predicate)
    {
        return context.Set<T>().AsNoTracking().SingleOrDefault(predicate);
    }

    public T Created(T entity)
    {
        if (entity is null)
            throw new Exception("Entity is null");
        context.Set<T>().Add(entity);
        return entity;
        
    }

    public T Update(T entity)
    {
        if (entity is null)
            throw new Exception("Entity is null");
        context.Set<T>().Update(entity);
        return entity;

    }

    public T Delete(T entity)
    {
        if (entity is null)
            throw new Exception("Entity is null");
        context.Set<T>().Remove(entity);
        return entity;

    }
}