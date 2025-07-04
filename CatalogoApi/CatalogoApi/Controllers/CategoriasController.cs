using CatalogoApi.Data;
using CatalogoApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogoApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        public CategoriasController(CatalogoContext context)
        {
            _context= context;
        }
        private readonly CatalogoContext _context;

        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> Get()
        {
            IEnumerable<Categoria>categorias=_context.Categorias.ToList();
            if (categorias is null)
                return NotFound("A lista de categorias esta vazia");

            return Ok(categorias);
        }

        [HttpGet("{id:int}",Name ="obter")]
        public ActionResult<Categoria> Get(int id)
        {
            var categoria = _context.Categorias.FirstOrDefault(c => c.Id == id);
            if (categoria is null)
                return NotFound("Essa categoria não foi encontrada");
            return Ok(categoria);
        }

        [HttpGet("produtos")]
        public ActionResult<IEnumerable<Categoria>> GetCategoriasProdutos()
        {
            return _context.Categorias.Include(x => x.Produtos).ToList();
        }

        [HttpPost]
        public ActionResult<Categoria> Post(Categoria categoria)
        {
            if (categoria is null)
                return BadRequest();
            _context.Categorias.Add(categoria);
            _context.SaveChanges();
            return CreatedAtRoute("obter",new {categoria.Id},categoria);
        }

        [HttpPut("{id:int}")]
        public ActionResult<Categoria> Put(int id, Categoria categoria)
        {
            if(id != categoria.Id)
                return BadRequest();
            if (categoria is null)
                return NotFound();

            _context.Entry(categoria).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok(categoria);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<Categoria> Delete(int id)
        {
            var categoria = _context.Categorias.FirstOrDefault(x => x.Id == id);
            if (categoria is null)
                return NotFound("Categoria não encontrada!");
            _context.Categorias.Remove(categoria);
            _context.SaveChanges();
            return Ok(categoria);
        }
    }
}
