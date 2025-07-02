using CatalogoApi.Data;
using CatalogoApi.Model;
using Microsoft.AspNetCore.Mvc;

namespace CatalogoApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutosController : ControllerBase
    {
        public ProdutosController(CatalogoContext context)
        {
            _context = context;
        }
        private readonly CatalogoContext _context;

        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get()
        {
            var produtos = _context.Produtos.ToList();
            if (produtos is null)
                return NotFound("Produtos não encontrado");
            return produtos;
        }

        [HttpGet("{id:int}")]
        public ActionResult<Produto> Get(int id)
        {
            var produto = _context.Produtos.SingleOrDefault(x => x.Id == id);
            if (produto is null)
                return NotFound("Produto não existe");
            return produto;
        }
    }
}
