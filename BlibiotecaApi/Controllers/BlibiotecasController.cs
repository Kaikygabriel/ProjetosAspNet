using BlibiotecaApi.Data;
using BlibiotecaApi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlibiotecaApi.Controllers;

[ApiController]
[Route("[controller]")]
public class BlibiotecasController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult> GetAsync([FromServices]BlibiotecaContext context)
    {
        try
        {
            IEnumerable<Blibioteca> blibiotecas = await context.Blibiotecas.AsNoTracking().ToListAsync();
            if (blibiotecas is null)
                return NotFound();
            return Ok(blibiotecas);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error ao processar a requisição");
        }
     
    }

    [HttpGet("{id:int:min(1)}")]
    public async Task<ActionResult> GetAsync(int id, [FromServices] BlibiotecaContext context)
    {
        try
        {
            var blibioteca = await context.Blibiotecas.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
            if (blibioteca is null)
                return NotFound("Blibioteca não encontrada");
            return Ok(blibioteca);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error ao processar a requisição");
        }
   
    }

    [HttpPost]
    public ActionResult Post([FromBody]Blibioteca blibioteca, [FromServices] BlibiotecaContext context)
    {
        try
        {
            context.Blibiotecas.Add(blibioteca);
            context.SaveChanges();
            return Created();
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error ao processar a requisição");
        }
    }
}