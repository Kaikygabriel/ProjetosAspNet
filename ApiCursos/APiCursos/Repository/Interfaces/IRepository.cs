using System.Linq.Expressions;
using APiCursos.Model;

namespace ApiCursos.Repository.Interfaces;

public interface IRepository<T> where T: class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIDAsync(Expression<Func<T,bool>> predicate);
    Task<T> CreateAsync(T entity);
    T Update(T entity);
    T Delete(T entity);
}