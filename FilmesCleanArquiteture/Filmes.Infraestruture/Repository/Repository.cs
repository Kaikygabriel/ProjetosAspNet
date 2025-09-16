using System.Linq.Expressions;
using Filmes.Domain.Interfaces;
using Filmes.Infraestruture.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Filmes.Infraestruture.Repository;

public class Repository<T> : IRepository<T> where T: class
{
    protected readonly AppDbContext context;
    public Repository(AppDbContext context)
    {
        this.context = context;
    }

    public async Task<List<T>> GetAll(CancellationToken cancellationToken)
    {
        return await context.Set<T>().AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<T?> GetByPredicate(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
    {
        return await context.Set<T>().AsNoTracking().FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public void Create(T entity)
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