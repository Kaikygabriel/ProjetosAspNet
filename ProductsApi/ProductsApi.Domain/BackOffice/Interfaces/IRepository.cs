using System.Linq.Expressions;
using ProductsApi.Domain.BackOffice.ObjectValue;

namespace ProductsApi.Domain.BackOffice.Interfaces;

public interface IRepository<T> where T : IAggregateRoot
{
    Task<IEnumerable<T>> GetAll(QueryStringParameters parameters);
    Task<T?> GetByPredicate(Expression<Func<T, bool>> predicate);
    void  Create(T entity);
    void Update(T entity);
    void Delete(T entity);
}