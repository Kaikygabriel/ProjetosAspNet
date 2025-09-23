using Catalogo.Domain.Interfaces;

namespace CatalogoXunitTest.Mocks;

public class FakeUnitOfWork  : IUnitOfWork
{
    public IRepositoryProduto RepositoryProduto { get; } = new FakeRepositoryProduto();
    public IRepositoryCategoria RepositoryCategoria { get; }
    public IRepositoryUser RepositoryUser { get; }
    public async Task CommitAsync()
    {
        await Task.Delay(0);
    }
}