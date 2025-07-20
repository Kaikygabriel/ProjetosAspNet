using CatalogoApi.Model;

namespace CatalogoApi.Repository.Interface
{
    public interface IRepositoryCategoria  : IRepository<Categoria> 
    {
        IEnumerable<Categoria> GetCategoriasProdutos();
    }
}
