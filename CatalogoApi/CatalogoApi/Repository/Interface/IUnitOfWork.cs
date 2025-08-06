namespace CatalogoApi.Repository.Interface
{
    public interface IUnitOfWork
    {
        IRepositoryProduto ProdutoRepository { get; }
        IRepositoryCategoria CategoriaRepository { get; }
        Task CommitAsync();
    }
}
