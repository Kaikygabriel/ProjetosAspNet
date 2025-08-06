using CatalogoApi.Model;
using CatalogoApi.Model.Dto;
using CatalogoApi.Pagination;

namespace CatalogoApi.Repository.Interface
{
    public interface IRepositoryProduto : IRepository<Produto>
    {
        Task<PagedList<Produto>> GetAllProductAsync(ProdutosPagination pagination);
        Task<PagedList<Produto>>  GetProdutosFiltroPrecoAsync(ProdutosFiltroPreco produtosFiltro);

    }
}
