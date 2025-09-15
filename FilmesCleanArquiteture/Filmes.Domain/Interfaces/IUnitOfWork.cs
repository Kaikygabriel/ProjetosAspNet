namespace Filmes.Domain.Interfaces;

public interface IUnitOfWork
{
    public IRepositoryFilme RepositoryFilme { get; }
    Task CommitAsync(CancellationToken cancellationToken);
}