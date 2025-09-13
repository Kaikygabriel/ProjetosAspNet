using CatalogoApi.Repository.Interface;

namespace CatalogoApiTests.Mocks;

public class FakeUnitOfWork  : IUnitOfWork
{
    public IRepositoryProduto ProdutoRepository { get; } = new FakeProdutoRepository();
    public IRepositoryCategoria CategoriaRepository { get; }
    public async Task CommitAsync()
    {
        await Task.Delay(0);
    }
}