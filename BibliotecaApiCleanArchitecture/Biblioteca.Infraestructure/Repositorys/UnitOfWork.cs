using Biblioteca.Domain.BackOffice.Interfaces;
using Biblioteca.Infraestructure.Context;

namespace Biblioteca.Infraestructure.Repositorys;

public class UnitOfWork : IUnitOfWork
{
    private IRepositoryBook _repositoryBook;
    private IRepositoryUser _repositoryUser;
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

    public IRepositoryBook RepositoryBook
    {
        get
        {
            return _repositoryBook = _repositoryBook ?? new RepositoryBook(context);
        }
    }

    public async Task CommitAsync()
    {
        await context.SaveChangesAsync();
    }
}