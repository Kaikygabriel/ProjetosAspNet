namespace Catalogo.Domain.Interfaces;

public interface IUnitOfWork
{
    public IRepositoryProduto RepositoryProduto { get; }
    public IRepositoryCategoria RepositoryCategoria { get; }
    Task CommitAsync();
}