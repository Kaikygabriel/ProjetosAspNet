using System.Linq.Expressions;

namespace Filmes.Domain.Interfaces;

public interface IRepository<T> where T : class
{
    Task<List<T>> GetAll(CancellationToken cancellationToken);
    Task<T> GetByPredicate(Expression<Func<T,bool>>predicate,CancellationToken cancellationToken);
    void Create(T entity);
    void Update(T entity);
    void Delete(T entity);
}