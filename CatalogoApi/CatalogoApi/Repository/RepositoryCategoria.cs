using CatalogoApi.Data;
using CatalogoApi.Model;
using Microsoft.EntityFrameworkCore;

namespace CatalogoApi.Repository
{
    public class RepositoryCategoria : IRepositoryCategoria
    {
        private readonly CatalogoContext _context;

        public RepositoryCategoria(CatalogoContext context)
        {
          _context = context;
        }

        public IEnumerable<Categoria> GetCategorias()
        {
            return _context.Categorias.Take(10).AsNoTracking().ToList();
        }
        public IEnumerable<Categoria> GetCategoriasProdutos()
        {
           return _context.Categorias.Take(10).Include(x => x.Produtos).AsNoTracking().ToList();
        }
        public Categoria? GetCategoria(int Id)
        {
            return _context.Categorias.AsNoTracking().SingleOrDefault(x => x.Id == Id);
        }

        public Categoria Create(Categoria categoria)
        {
            if (categoria is null)
                throw new Exception(nameof(categoria));
            _context.Categorias.Add(categoria);
            _context.SaveChanges();
            return categoria;
        }

        public Categoria Update(Categoria categoria)
        {
            if (categoria is null)
                throw new Exception(nameof(categoria));
            _context.Categorias.Update(categoria);
            _context.SaveChanges();
            return categoria;
        }

        public Categoria Delete(int Id)
        {
            var categoria = GetCategoria(Id);
            if (categoria is null)
                throw new Exception(nameof(categoria));
            _context.Categorias.Remove(categoria);
            _context.SaveChanges();
            return categoria;
        }
    }
}
