using CatalogoApi.Model;
using CatalogoApi.Pagination;

namespace CatalogoApi.Repository.Interface
{
    public interface IRepositoryCategoria  : IRepository<Categoria> 
    {
        IEnumerable<Categoria> GetCategoriasProdutos();
        PagedList<Categoria> GetAllCategoria(CategoriaPagination pagination);
        PagedList<Categoria> GetCategoriaFiltroName(CategoriaFiltroName pagination);

    }
}
