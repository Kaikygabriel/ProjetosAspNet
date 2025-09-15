using Filmes.Domain.Interfaces;
using Filmes.Infraestruture.Data;

namespace Filmes.Infraestruture.Repository;

public class UnitOfWork : IUnitOfWork
{
    private RepositoryFilme _repositoryFilme;
    private readonly AppDbContext context;

    public IRepositoryFilme RepositoryFilme
    {
        get
        {
            return _repositoryFilme = _repositoryFilme ?? new RepositoryFilme(context);
        }
    }

    public async Task CommitAsync(CancellationToken cancellationToken)
    {
        await context.SaveChangesAsync(cancellationToken);
    }
}