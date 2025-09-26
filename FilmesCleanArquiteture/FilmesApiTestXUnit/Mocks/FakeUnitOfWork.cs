using Filmes.Domain.Interfaces;

namespace FilmesApiTestXUnit.Mocks;

public class FakeUnitOfWork : IUnitOfWork
{
    public IRepositoryUser RepositoryUser { get; } = new FakeUserRepository();
    public IRepositoryFilme RepositoryFilme { get; } = new FakeRepositoryFilmes();
    public async Task CommitAsync()
    {
        await Task.Delay(0);
    }
}