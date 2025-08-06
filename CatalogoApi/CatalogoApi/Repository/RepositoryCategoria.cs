using CatalogoApi.Data;
using CatalogoApi.Model;
using CatalogoApi.Pagination;
using CatalogoApi.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace CatalogoApi.Repository
{
    public class RepositoryCategoria : Repository<Categoria>, IRepositoryCategoria
    {
        public RepositoryCategoria(CatalogoContext context) : base(context)
        {
        }
        
        public async Task<IEnumerable<Categoria>> GetCategoriasProdutosAsync()
        {
            return await _context.Categorias.Include(x => x.Produtos).AsNoTracking().ToListAsync();
        }

        public async Task<PagedList<Categoria>> GetAllCategoria(CategoriaPagination pagination)
        {
            var list = await GetAllAsync();
            var listOrdenadaCategoria = PagedList<Categoria>.ToPagedList(list
                , pagination.PageNumber
                , pagination.PageSize);
            return listOrdenadaCategoria;
        }

        public async Task<PagedList<Categoria>> GetCategoriaFiltroName(CategoriaFiltroName pagination)
        {
            var list = await GetAllAsync();
            if (!string.IsNullOrEmpty(pagination.Name))
                list = list.Where(x => x.Nome.Contains(pagination.Name,StringComparison.InvariantCultureIgnoreCase));
            return PagedList<Categoria>.ToPagedList(list, pagination.PageNumber, pagination.PageSize);
        }
    }
}
