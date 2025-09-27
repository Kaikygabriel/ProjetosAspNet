using System.Linq.Expressions;
using Filmes.Domain.Entities;

namespace Filmes.Application.Interfaces;

public interface IFilmeServiceRepository
{
    Task<Filme?> GetByPredicate(Expression<Func<Filme,bool>>predicate);
    Task<IEnumerable<Filme>> GetAll();
    Task Create(Filme entity);
    Task Update(Filme entity);
    Task Delete(Filme entity);
}