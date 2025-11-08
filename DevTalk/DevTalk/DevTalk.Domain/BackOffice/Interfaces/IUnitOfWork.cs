using DevTalk.Domain.BackOffice.Interfaces.Message;
using DevTalk.Domain.BackOffice.Interfaces.User;

namespace DevTalk.Domain.BackOffice.Interfaces;

public interface IUnitOfWork
{
     IRepositoryMessage RepositoryMessage { get; }
     IRepositoryUser RepositoryUser { get; }
    Task CommitAsync();
}