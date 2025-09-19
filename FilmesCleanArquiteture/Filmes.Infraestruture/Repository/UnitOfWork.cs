using Filmes.Domain.Interfaces;
using Filmes.Infraestruture.Data;

namespace Filmes.Infraestruture.Repository;

public class UnitOfWork : IUnitOfWork
{
    private RepositoryUser _repositoryUser;

    private RepositoryFilme _repositoryFilme;
    private readonly AppDbContext context;

    public UnitOfWork(AppDbContext context)
    {
        this.context = context;
    }

    public IRepositoryUser RepositoryUser
    {
        get
        {
            return _repositoryUser = _repositoryUser ?? new RepositoryUser(context);
        }
    }

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