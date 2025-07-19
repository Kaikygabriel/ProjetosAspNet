using CatalogoApi.Model;

namespace CatalogoApi.Repository
{
    public interface IRepositoryProduto
    {
        IEnumerable<Produto> GetProdutos();
        Produto GetProduto(int Id);
        Produto Create(Produto produto);
        Produto Update(Produto produto);
        Produto Delete(int Id);
    }
}
