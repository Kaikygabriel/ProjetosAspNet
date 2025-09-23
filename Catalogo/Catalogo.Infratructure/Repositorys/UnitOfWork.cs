using Catalogo.Domain.Interfaces;
using Catalogo.Infratructure.Context;

namespace Catalogo.Infratructure.Repositorys;

public class UnitOfWork : IUnitOfWork
{
    private RepositoryUser _repositoryUserImplement;
    private RepositoryCategoria _repositoryCategoriaImplement;
    private RepositoryProduto _repositoryProdutoImplement;
    private readonly AppDbContext context;

    public UnitOfWork(AppDbContext context)
    {
        this.context = context;
    }

    public UnitOfWork()
    {
        
    }

    public IRepositoryProduto RepositoryProduto
    {
        get
        {
            return _repositoryProdutoImplement = _repositoryProdutoImplement ?? new RepositoryProduto(context);
        }
    }

    public IRepositoryCategoria RepositoryCategoria
    {
        get
        {
            return _repositoryCategoriaImplement =  _repositoryCategoriaImplement?? new RepositoryCategoria(context);
        }
    }

    public IRepositoryUser RepositoryUser
    {
        get
        {
            return _repositoryUserImplement = _repositoryUserImplement ?? new RepositoryUser(context);
        }
    }

    public async  Task CommitAsync()
    {
        await context.SaveChangesAsync();
    }
}