using Filmes.Domain.Interfaces;

namespace FilmesApiTestXUnit.Mocks;

public class FakeUnitOfWork : IUnitOfWork
{
    public IRepositoryFilme RepositoryFilme { get; } = new FakeRepositoryFilmes();
    public async Task CommitAsync(CancellationToken cancellationToken)
    {
        await Task.Delay(0);
    }
}