using Filmes.Domain.Interfaces;

namespace FilmesApiTestXUnit.Mocks;

public class FakeUnitOfWork : IUnitOfWork
{
    public IRepositoryUser RepositoryUser { get; }
    public IRepositoryFilme RepositoryFilme { get; } = new FakeRepositoryFilmes();
    public async Task CommitAsync(CancellationToken cancellationToken)
    {
        await Task.Delay(0);
    }
}