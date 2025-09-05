using System.Linq.Expressions;

namespace AcademyPro.Repository.Interfaces;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAll();
    Task<T> GetByPredicate(Expression<Func<T, bool>> preditace);
    void Create(T Entity);
    void Update(T Entity);
    void Delete(T Entity);
}