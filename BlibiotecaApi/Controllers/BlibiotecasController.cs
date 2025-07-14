using BlibiotecaApi.Data;
using BlibiotecaApi.Migrations;
using BlibiotecaApi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlibiotecaApi.Controllers;

[ApiController]
[Route("[controller]")]
public class BlibiotecasController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Blibioteca>>> GetAsync([FromServices] BlibiotecaContextApi context)
    {
        IEnumerable<Blibioteca> blibiotecas = await context.Blbiotecas.AsNoTracking().ToListAsync();
        return Ok(blibiotecas);
    }

    [HttpPost]
    public ActionResult Post([FromServices] BlibiotecaContextApi context,Blibioteca blibioteca)
    {
        context.Blbiotecas.Add(blibioteca);
        context.SaveChanges();
        return Created();
    }
}