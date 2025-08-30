using System.Linq.Expressions;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using NotifiMe.Data;
using NotifiMe.Repository.Interface;

namespace NotifiMe.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly AppDbContext context;

    public Repository(AppDbContext context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await context.Set<T>().AsNoTracking().ToListAsync();
    }

    public async Task<T?> GetByPredicateAsync(Expression<Func<T, bool>> predicate)
    {
        return await context.Set<T>().AsNoTracking().FirstOrDefaultAsync(predicate);
    }

    public virtual void Create(T entity)
    {
        if (entity is null)
            throw new Exception("Entity is null");
        context.Set<T>().Add(entity);
    }

    public void Update(T entity)
    {
        
        if (entity is null)
            throw new Exception("Entity is null");
        context.Set<T>().Update(entity);
    }

    public void Delete(T entity)
    {
        
        if (entity is null)
            throw new Exception("Entity is null");
        context.Set<T>().Remove(entity);
    }
}