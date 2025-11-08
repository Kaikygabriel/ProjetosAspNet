using Barbearia.Domain.BackOffice.Interfaces.Roles;
using Barbearia.Domain.BackOffice.Interfaces.Users;

namespace Barbearia.Domain.BackOffice.Interfaces;

public interface IUnitOfWork
{
    public IRepositoryUser RepositoryUser { get;}
    public IRepositoryRole RepositoryRole { get;}
    Task CommitAsync();
}