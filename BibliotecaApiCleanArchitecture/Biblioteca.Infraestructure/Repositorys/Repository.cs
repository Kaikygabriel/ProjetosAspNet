using System.Linq.Expressions;
using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Interfaces;
using Biblioteca.Infraestructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Infraestructure.Repositorys;

public class Repository<T> : IRepository<T> where T : Entity
{
    protected readonly AppDbContext context;

    public Repository(AppDbContext context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        return await context.Set<T>().AsNoTracking().ToListAsync();
    }

    public async Task<T?> GetByPredicate(Expression<Func<T?, bool>> predicate)
    {
        return await context.Set<T>().AsNoTracking().FirstOrDefaultAsync(predicate);
    }

    public virtual void  Create(T entity)
    {
        context.Add(entity);
    }

    public void Update(T entity)
    {
        context.Update(entity);
    }

    public void Delete(T entity)
    {
        context.Remove(entity);
    }
}