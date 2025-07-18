using CatalogoApi.Model;

namespace CatalogoApi.Repository
{
    public interface IRepositoryCategoria
    {
        IEnumerable<Categoria> GetCategorias();
        IEnumerable<Categoria> GetCategoriasProdutos();
        Categoria GetCategoria(int Id);
        Categoria Create(Categoria categoria);
        Categoria Update(Categoria categoria);
        Categoria Delete(int Id);
    }
}
