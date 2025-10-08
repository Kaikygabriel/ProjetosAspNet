using System.Linq.Expressions;
using EduCore.Domain.Entities;

namespace EduCore.Domain.Interfaces;

public interface IRepository<T> where T  :Entity
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByPredicateAsync(Expression<Func<T, bool>>predicate);
    void Create(T entity);
    void Update(T entity);
    void Delete(T entity);
}