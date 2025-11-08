using DevTalk.Domain.BackOffice.Interfaces;
using DevTalk.Domain.BackOffice.Interfaces.Message;
using DevTalk.Domain.BackOffice.Interfaces.User;

namespace DevTalk.Test.Mock;

public class FakeUnitOfWork :  IUnitOfWork
{
    public IRepositoryMessage RepositoryMessage { get; }
    public IRepositoryUser RepositoryUser { get; } = new FakeRepositoryUser();
    public async Task CommitAsync()
        => await Task.Delay(0);
    
}