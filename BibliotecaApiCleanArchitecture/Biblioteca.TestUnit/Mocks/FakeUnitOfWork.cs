using Biblioteca.Domain.BackOffice.Interfaces;

namespace Biblioteca.TestUnit.Mocks;

public class FakeUnitOfWork : IUnitOfWork
{
    public IRepositoryUser RepositoryUser { get; } = new FakeRepositoryUser();
    public IRepositoryBook RepositoryBook { get; }
    public async Task CommitAsync()
    {
        await Task.Delay(0);
    }
}