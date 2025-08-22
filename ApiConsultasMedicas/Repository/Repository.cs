using System.Linq.Expressions;
using ApiConsultasMedicas.Data;
using ApiConsultasMedicas.Repository.Interface;
using Microsoft.EntityFrameworkCore;



namespace ApiConsultasMedicas.Repository;

public class Repository<T> : IRepository<T> where T : class, new()
{
    protected readonly ApiConsultaContext context;

    public Repository(ApiConsultaContext context)
    {
        this.context = context;
    }

    public void Create(T? entity)
    {
        if (entity is null)
            throw new Exception();
        context.Set<T>().Add(entity);
    }

    public void Delete(T? entity)
    {
          if (entity is null)
            throw new Exception();
        context.Set<T>().Remove(entity);
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        return await context.Set<T>().AsNoTracking().ToListAsync();
    }

    public async Task<T?> GetById(Expression<Func<T, bool>> predicate)
    {
        return await context.Set<T>().AsNoTracking().SingleOrDefaultAsync(predicate);
    }

    public void Update(T? entity)
    {
          if (entity is null)
            throw new Exception();
        context.Set<T>().Update(entity);
    }
}