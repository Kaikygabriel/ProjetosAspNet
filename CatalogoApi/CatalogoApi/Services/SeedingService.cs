using CatalogoApi.Data;
using CatalogoApi.Model;

namespace CatalogoApi.Services
{
    public class SeedingService
    {
        public SeedingService(CatalogoContext context)
        {
            _context = context;
        }
        private readonly CatalogoContext? _context;

        public void Seed()
        {
            if (_context.Categorias.Any() || _context.Categorias.Any())
                return;

            var c1 = new Categoria(1, "Hardware", "hardware.jpg");
            var c2 = new Categoria(2, "Ferramentas", "ferramentas.jpg");

            _context.Categorias.AddRange(c1, c2);
            _context.SaveChanges();

            var p1 = new Produto(1,"Rx580","Placa de video",500.0m,"rx580.jpg",10,DateTime.Now,c1.Id,c1);
            var p2 = new Produto(2, "pá", "Pá para cavar", 50.0m, "pa.jpg", 1, DateTime.Now, c2.Id, c2);

            _context.Produtos.AddRange(p1,p2);
            _context.SaveChanges();
        }
    }
}
