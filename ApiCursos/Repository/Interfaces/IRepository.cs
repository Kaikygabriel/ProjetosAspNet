using System.Linq.Expressions;

namespace ApiCursos.Repository.Interfaces;

public interface IRepository<T> where T: class
{
    IEnumerable<T> GetAll();
    T? GetByID(Expression<Func<T, bool>> predicate);
    T Create(T entity);
    T Update(T entity);
    T Delete(T entity);
}