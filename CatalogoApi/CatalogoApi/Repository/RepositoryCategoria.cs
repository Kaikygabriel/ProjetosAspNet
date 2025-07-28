using CatalogoApi.Data;
using CatalogoApi.Model;
using CatalogoApi.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace CatalogoApi.Repository
{
    public class RepositoryCategoria :Repository<Categoria>, IRepositoryCategoria
    {
        public RepositoryCategoria(CatalogoContext context) : base(context)
        {
        }
        
        public IEnumerable<Categoria> GetCategoriasProdutos(int skip=0,int take=10)
        {
            if (take > 50)
                take = 50;
            return _context.Categorias.Include(x => x.Produtos).AsNoTracking().Skip(skip).Take(take).ToList();
        }
    }
}
