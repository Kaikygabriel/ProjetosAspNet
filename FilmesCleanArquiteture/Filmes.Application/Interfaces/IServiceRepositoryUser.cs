using Filmes.Domain.Interfaces;
using Filmes.Domain.Entities;

namespace Filmes.Application.Interfaces;

public interface IServiceRepositoryUser
{
    Task<User?> GetByName(string name);
    Task<IEnumerable<User>> GetAll();
    Task Create(User entity);
    Task Update(User entity);
    Task Delete(User entity);
}