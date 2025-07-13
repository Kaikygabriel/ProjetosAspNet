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
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetAsync()
        {
            try 
            {
                IEnumerable<Categoria> categorias = await _context.Categorias.AsNoTracking().ToListAsync();
                if (categorias is null)
                    return NotFound("A lista de categorias esta vazia");

                return Ok(categorias);
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorrreu um erro ao tratar a solicitação");
            }
        }

        [HttpGet("{id:int:min(1)}",Name ="obter")]
        public async Task<ActionResult<Categoria>> GetAsync(int id)
        {
            try
            {
                var categoria = await _context.Categorias.AsNoTracking().SingleOrDefaultAsync(c => c.Id == id);
                if (categoria is null)
                    return NotFound("Essa categoria não foi encontrada");
                return Ok(categoria);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorrreu um erro ao tratar a solicitação");
            }
        }

        [HttpGet("produtos")]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetCategoriasProdutosAsync()
        {
            try
            {
                return await _context.Categorias.Include(x => x.Produtos).Where(x => x.Id <= 5).AsNoTracking().ToListAsync();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorrreu um erro ao tratar a solicitação");
            }
        }

        [HttpPost]
        public ActionResult<Categoria> Post(Categoria categoria)
        {
            try
            {
                if (categoria is null)
                    return BadRequest();
                _context.Categorias.Add(categoria);
                _context.SaveChanges();
                return CreatedAtRoute("obter", new { categoria.Id }, categoria);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorrreu um erro ao tratar a solicitação");
            }
        }

        [HttpPut("{id:int}")]
        public ActionResult<Categoria> Put(int id, Categoria categoria)
        {
            try
            {
                if (id != categoria.Id)
                    return BadRequest();
                if (categoria is null)
                    return NotFound();

                _context.Entry(categoria).State = EntityState.Modified;
                _context.SaveChanges();
                return Ok(categoria);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorrreu um erro ao tratar a solicitação");
            }
        }

        [HttpDelete("{id:int}")]
        public ActionResult<Categoria> Delete(int id)
        {
            try
            {
                var categoria = _context.Categorias.FirstOrDefault(x => x.Id == id);
                if (categoria is null)
                    return NotFound("Categoria não encontrada!");
                _context.Categorias.Remove(categoria);
                _context.SaveChanges();
                return Ok(categoria);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorrreu um erro ao tratar a solicitação");
            }
        }
    }
}
