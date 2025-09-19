namespace Filmes.Domain.Interfaces;

public interface IUnitOfWork
{
    public IRepositoryUser RepositoryUser { get; }

    public IRepositoryFilme RepositoryFilme { get; }
    Task CommitAsync(CancellationToken cancellationToken);
}