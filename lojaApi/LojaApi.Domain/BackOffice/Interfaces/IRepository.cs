using System.Linq.Expressions;
using LojaApi.Domain.BackOffice.Entitys;

namespace LojaApi.Domain.BackOffice.Interfaces;

public interface IRepository <T> where T: Entity
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByPredicate(Expression<Func<T, bool>>predicate);
    void Create(T entity);
    void Update(T entity);
    void Delete(T entity);
}