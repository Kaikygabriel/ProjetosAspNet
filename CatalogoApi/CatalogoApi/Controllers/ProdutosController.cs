using CatalogoApi.Data;
using CatalogoApi.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : ControllerBase
    {
        public ProdutosController(CatalogoContext context)
        {
            _context = context;
        }
        private readonly CatalogoContext _context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> GetAsync() 
        {
            var produtos = await _context.Produtos.Take(10).AsNoTracking().ToListAsync();
            if (produtos is null)
                return NotFound("Produtos não encontrado...");
            return produtos;
        }
         
        [HttpGet("{id:int:min(1)}",Name = "ObterProduto")]
        public async Task<ActionResult<Produto>> GetAsync(int id)
        {
            var produto =  await _context.Produtos.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
            if (produto is null)
                return NotFound("Produto não Encontrado...");
            return produto;
        }

        [HttpPost]
        public ActionResult Post(Produto produto)
        {
            if (produto is null)
                return BadRequest("Produto nulo");
            _context.Produtos.Add(produto);
            _context.SaveChanges();
            return new CreatedAtRouteResult("ObterProduto", new {produto.Id},produto);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id,Produto produto)
        {
            if (id != produto.Id)
                return BadRequest();

            _context.Entry(produto).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok(produto);
        }
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var produto = _context.Produtos.SingleOrDefault(x => x.Id == id);

            if (produto is null)
                return NotFound("Produto não encontrado...");
            _context.Produtos.Remove(produto);
            _context.SaveChanges();
            return Ok(produto);
        }
    }
}
