using System.Linq.Expressions;
using AcademyPro.Data;
using AcademyPro.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AcademyPro.Repository;

public class Repository<T>(AppDbContext context) : IRepository<T> where T : class 
{
    
    public async Task<IEnumerable<T>> GetAll()
    {
        return await context.Set<T>().AsNoTracking().ToListAsync();
    }

    public async Task<T?> GetByPredicate(Expression<Func<T, bool>> preditace)
    {
        return await context.Set<T>().AsNoTracking().FirstOrDefaultAsync(preditace);
    }

    public void Create(T? Entity)
    {
        if (Entity is null)
            throw new ArgumentNullException(nameof(Entity));
        context.Set<T>().Add(Entity);
    }

    public void Update(T? Entity)
    {
        if (Entity is null)
            throw new ArgumentNullException(nameof(Entity));
        context.Set<T>().Update(Entity);
    }

    public void Delete(T? Entity)
    {
        if (Entity is null)
            throw new ArgumentNullException(nameof(Entity));
        context.Set<T>().Remove(Entity);
    }
}