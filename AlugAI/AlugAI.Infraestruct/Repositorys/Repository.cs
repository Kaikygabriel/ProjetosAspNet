using System.Linq.Expressions;
using AlugAI.Domain.Entities;
using AlugAI.Domain.Interfaces;
using AlugAI.Infraestruct.Context;
using Microsoft.EntityFrameworkCore;

namespace AlugAI.Infraestruct.Repositorys;

public class Repository<T> : IRepository<T> where T : Entity
{
    private readonly AppDbContext context;

    public Repository(AppDbContext context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        return await context.Set<T>().AsNoTracking().ToListAsync();
    }

    public async Task<T?> GetByPredicate(Expression<Func<T,bool>> predicate)
    {
        return await context.Set<T>().AsNoTracking().FirstOrDefaultAsync(predicate);
    }

    public void Create(T entity)
    {
        context.Set<T>().Add(entity);
    }

    public void Update(T entity)
    {
        context.Set<T>().Update(entity);
    }

    public void Delete(T entity)
    {
        context.Set<T>().Remove(entity);
    }
}