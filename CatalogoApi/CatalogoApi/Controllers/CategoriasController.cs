using System.Threading.Tasks;
using CatalogoApi.Data;
using CatalogoApi.Filters;
using CatalogoApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace CatalogoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        public CategoriasController(CatalogoContext context)
        {
            _context= context;
        }
        private readonly CatalogoContext _context;

        [HttpGet]
        //[ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetAsync()
        {  
                IEnumerable<Categoria> categorias = await _context.Categorias.AsNoTracking().ToListAsync();
                if (categorias is null)
                    return NotFound("A lista de categorias esta vazia");

                return Ok(categorias);
        }

        [HttpGet("{id:int:min(1)}",Name ="obter")]
        public async Task<ActionResult<Categoria>> GetAsync(int id)
        {
                var categoria = await _context.Categorias.AsNoTracking().SingleOrDefaultAsync(c => c.Id == id);
                if (categoria is null)
                    return NotFound("Essa categoria não foi encontrada");
                return Ok(categoria);
        }

        [HttpGet("produtos")]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetCategoriasProdutosAsync()
        {
            return await _context.Categorias.Take(10).Include(x => x.Produtos)
                .AsNoTracking().ToListAsync();
        }

        [HttpPost]
        public ActionResult<Categoria> Post(Categoria categoria)
        {

                if (categoria is null)
                    return BadRequest();
                _context.Categorias.Add(categoria);
                _context.SaveChanges();
                return CreatedAtRoute("obter", new { categoria.Id }, categoria);
        }

        [HttpPut("{id:int:min(1)}")]
        public ActionResult<Categoria> Put(int id, Categoria categoria)
        {
                if (id != categoria.Id)
                    return BadRequest();
                if (categoria is null)
                    return NotFound();

                _context.Entry(categoria).State = EntityState.Modified;
                _context.SaveChanges();
                return Ok(categoria);
        }

        [HttpDelete("{id:int:min(1)}")]
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
