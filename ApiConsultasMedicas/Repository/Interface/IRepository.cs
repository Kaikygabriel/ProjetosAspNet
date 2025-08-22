using System.Linq.Expressions;

namespace ApiConsultasMedicas.Repository.Interface;

public interface IRepository<T> where T : class, new()
{
    Task<IEnumerable<T>> GetAll();
    Task<T> GetById(Expression<Func<T, bool>> predicate);
    void Create(T? entity);
    void Update(T? entity);
    void Delete(T? entity);
}