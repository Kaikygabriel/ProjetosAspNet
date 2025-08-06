using System.Linq.Expressions;

namespace CatalogoApi.Repository.Interface
{
    public interface IRepository<T> where T:class 
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(Expression<Func<T,bool>>predicate);
        T Create(T entity);
        T Update(T entity);
        T Delete(T entity);
    }
}
