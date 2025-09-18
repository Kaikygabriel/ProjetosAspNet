using System.Linq.Expressions;
using Filmes.Domain.Entities;

namespace Filmes.Domain.Interfaces;

public interface IRepository<T> where T : Entity
{
    Task<IEnumerable<T>> GetAll(CancellationToken cancellationToken);
    Task<T> GetByPredicate(Expression<Func<T,bool>>predicate,CancellationToken cancellationToken);
    void Create(T entity);
    void Update(T entity);
    void Delete(T entity);
}