using BlibiotecaApi.Data;
using BlibiotecaApi.Model;
using BlibiotecaApi.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlibiotecaApi.Controllers;

[ApiController]
[Route("[controller]")]
public class LivrosController : ControllerBase
{
    [HttpGet]
    // [ServiceFilter(typeof(LoggingMenssageFilter))]
    public async Task<ActionResult<IEnumerable<Livro>>> GetAsync([FromServices] BlibiotecaContextApi context)
    {
        IEnumerable<Livro> livros = await context.Livros.Take(10).AsNoTracking().ToListAsync();
        return Ok(livros);
    }

    [HttpGet("{Id:int:min(1)}")]
    public async Task<ActionResult<Livro>> GetAsync(int Id, [FromServices] BlibiotecaContextApi context)
    {
        var livro = await context.Livros.AsNoTracking().SingleOrDefaultAsync(x => x.Id == Id);
        if (livro is null)
            return NotFound("Livro não encontrado");
        return Ok(livro);
    }

    [HttpPost]
    public ActionResult Post(Livro livro, [FromServices] BlibiotecaContextApi context)
    {
        context.Livros.Add(livro);
        context.SaveChanges();
        return Created();
    }

    [HttpDelete("{Id:int:min(1)}")]
    public ActionResult<Livro> Delete(int Id, [FromServices] BlibiotecaContextApi context)
    {
        var livro = context.Livros.SingleOrDefault(x => x.Id == Id);
        if (livro is null)
            return BadRequest("Livro não encontrado");
        context.Livros.Remove(livro);
        context.SaveChanges();
        return Ok(livro);
    }

    [HttpPut("{id:int:min(1)}")]
    public ActionResult Put(int id,
                            Livro livroAtt,
                            [FromServices] BlibiotecaContextApi context)
    {
        if (id != livroAtt.Id)
            return BadRequest("ID da rota é diferente de id do livro do body");
        context.Livros.Update(livroAtt);
        context.SaveChanges();
        return Ok(livroAtt);
    }
}