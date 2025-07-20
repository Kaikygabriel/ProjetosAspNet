using CatalogoApi.Data;
using CatalogoApi.Model;
using CatalogoApi.Repository.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : ControllerBase
    {
        public ProdutosController(IRepositoryProduto context)

        {
            _context = context;
        }
        private readonly IRepositoryProduto _context;

        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get() 
        {
            var produtos = _context.GetAll();
            if (produtos is null)
                return NotFound("Produtos não encontrado...");
            return Ok(produtos);
        }
         
        [HttpGet("{id:int:min(1)}",Name = "ObterProduto")]
        public ActionResult<Produto> Get(int id)
        {
            var produto = _context.GetById(p=>p.Id==id);
            if (produto is null)
                return NotFound("Produto não Encontrado...");
            return produto;
        }

        [HttpPost]
        public ActionResult Post(Produto produto)
        {
            if (produto is null)
                return BadRequest("Produto nulo");
            _context.Create(produto);
            return new CreatedAtRouteResult("ObterProduto", new {produto.Id},produto);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id,Produto produto)
        {
            if (id != produto.Id)
                return BadRequest();

            _context.Update(produto);
            return Ok(produto);
        }
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var produto = _context.GetById(p=>p.Id==id);
            if (produto is null)
                return NotFound("Produto não Encontrado...");
            var produtoExcluido= _context.Delete(produto);
            return Ok(produtoExcluido);
        }
    }
}
