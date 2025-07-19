using CatalogoApi.Data;
using CatalogoApi.Model;
using Microsoft.EntityFrameworkCore;

namespace CatalogoApi.Repository
{
    public class RepositoryProduto : IRepositoryProduto
    {
        private readonly CatalogoContext _context;


        public IEnumerable<Produto> GetProdutos()
        {
            return _context.Produtos.Take(10).AsNoTracking().ToList();
        }
        public Produto? GetProduto(int Id)
        {
            return _context.Produtos.AsNoTracking().SingleOrDefault(x => x.Id == Id);
        }

        public RepositoryProduto(CatalogoContext context)
        {
            _context = context;
        }

        public Produto Create(Produto produto)
        {
            if (produto is null)
                throw new Exception("Produto é null");
            _context.Produtos.Add(produto);
            _context.SaveChanges();
            return produto;
        }

        public Produto Update(Produto produto)
        {
            if (produto is null)
                throw new Exception("Produto é null");
            _context.Produtos.Update(produto);
            _context.SaveChanges();
            return produto;
        }

        public Produto Delete(int Id)
        {
            var produto = GetProduto(Id);
            if (produto is null)
                throw new Exception("Produto é null");
            _context.Produtos.Remove(produto);
            _context.SaveChanges();
            return produto;
        }
    }
}
