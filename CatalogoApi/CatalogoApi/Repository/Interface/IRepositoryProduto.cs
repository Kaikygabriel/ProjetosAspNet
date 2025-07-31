using CatalogoApi.Model;
using CatalogoApi.Model.Dto;
using CatalogoApi.Pagination;

namespace CatalogoApi.Repository.Interface
{
    public interface IRepositoryProduto : IRepository<Produto>
    {
        PagedList<Produto> GetAllProduct(ProdutosPagination pagination);
    }
}
