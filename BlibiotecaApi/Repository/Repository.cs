using System.Linq.Expressions;
using BlibiotecaApi.Data;
using BlibiotecaApi.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlibiotecaApi.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly BlibiotecaContextApi context;

    public Repository(BlibiotecaContextApi context)
    {
        this.context = context;
    }

    public void Create(T entity)
    {
        if (entity is null)
            throw new ArgumentNullException(nameof(entity));
        context.Set<T>().Add(entity);
    }

    public void Delete(T entity)
    {
        if (entity is null)
            throw new ArgumentNullException(nameof(entity));
        context.Set<T>().Remove(entity);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await context.Set<T>().AsNoTracking().ToListAsync();
    }

    public async Task<T?> GetByIdAsync(Expression<Func<T, bool>> predicate)
    {
        return await context.Set<T>().SingleOrDefaultAsync(predicate);
    }

    public void Update(T entity)
    {
        if (entity is null)
            throw new ArgumentNullException(nameof(entity));
        context.Set<T>().Update(entity);
    }
}