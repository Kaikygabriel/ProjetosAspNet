using CatalogoApi.Data;
using CatalogoApi.Model;
using CatalogoApi.Pagination;
using CatalogoApi.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace CatalogoApi.Repository
{
    public class RepositoryCategoria :Repository<Categoria>, IRepositoryCategoria
    {
        public RepositoryCategoria(CatalogoContext context) : base(context)
        {
        }
        
        public IEnumerable<Categoria> GetCategoriasProdutos()
        {
            return _context.Categorias.Include(x => x.Produtos).AsNoTracking().ToList();
        }

        public PagedList<Categoria> GetAllCategoria(CategoriaPagination pagination)
        {
            var listcategoria = _context.Categorias.AsNoTracking();
            var listOrdenadaCategoria = PagedList<Categoria>.ToPagedList(listcategoria
                , pagination.PageNumber
                , pagination.PageSize);
            return listOrdenadaCategoria;
        }
    }
}
