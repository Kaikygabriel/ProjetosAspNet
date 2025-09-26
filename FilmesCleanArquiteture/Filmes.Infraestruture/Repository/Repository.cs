using System.Linq.Expressions;
using Filmes.Domain.Entities;
using Filmes.Domain.Interfaces;
using Filmes.Infraestruture.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Filmes.Infraestruture.Repository;

public class Repository<T> : IRepository<T> where T: Entity
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

    public async Task<T?> GetByPredicate(Expression<Func<T, bool>> predicate)
    {
        return await context.Set<T>().AsNoTracking().FirstOrDefaultAsync(predicate);
    }

    public virtual void Create(T entity)
    {
        if (entity is null)
            throw new ArgumentNullException(nameof(entity));
        context.Set<T>().Add(entity);
    }

    public void Update(T entity)
    {
        if (entity is null)
            throw new ArgumentNullException(nameof(entity));
        context.Set<T>().Update(entity);
    }

    public void Delete(T entity)
    {
        if (entity is null)
            throw new ArgumentNullException(nameof(entity));
        context.Set<T>().Remove(entity);
    }
}