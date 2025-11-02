using System.Linq.Expressions;
using DevTalk.Domain.BackOffice.Entities;

namespace DevTalk.Domain.BackOffice.Interfaces;

public interface IRepository<T> where T : Entity
{
    Task<IEnumerable<T>> GetAll();
    Task<T> GetByPredicate(Expression<Func<T, bool>> predicate);
    void Create(T entity);
    void Update(T entity);
    void Delete(T entity);
}