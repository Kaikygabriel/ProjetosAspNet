using DevTalk.Domain.BackOffice.Interfaces.User;

namespace DevTalk.Domain.BackOffice.Interfaces;

public interface IUnitOfWork
{
    public IRepositoryUser RepositoryUser { get; }
    Task CommitAsync();
}