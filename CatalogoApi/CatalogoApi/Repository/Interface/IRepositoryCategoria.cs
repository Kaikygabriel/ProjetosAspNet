using CatalogoApi.Model;
using CatalogoApi.Pagination;

namespace CatalogoApi.Repository.Interface
{
    public interface IRepositoryCategoria  : IRepository<Categoria> 
    {
        Task<IEnumerable<Categoria>> GetCategoriasProdutosAsync();
        Task<PagedList<Categoria>> GetAllCategoria(CategoriaPagination pagination);
        Task<PagedList<Categoria>> GetCategoriaFiltroName(CategoriaFiltroName pagination);

    }
}
