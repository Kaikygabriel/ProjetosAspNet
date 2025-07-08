using BlibiotecaApi.Data;
using BlibiotecaApi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlibiotecaApi.Controllers;

[ApiController]
[Route("[controller]")]
public class LivrosController : ControllerBase
{
    public LivrosController(BlibiotecaContext context)
    {
        _context = context;
    }

    private readonly BlibiotecaContext _context;

    [HttpGet]
    public async Task<ActionResult> GetAsync()
    {
        IEnumerable<Livro> livros =await _context.Livros.AsNoTracking().ToListAsync();
        if (livros is null)
            return NotFound();
        return Ok(livros);
    }

    [HttpGet("{id:int:min(1)}",Name = "ObterLivro")]
    public async Task<ActionResult> GetAsync(int id)
    {
        var livro = await _context.Livros.AsNoTracking().SingleOrDefaultAsync(x=>x.Id==id);
        if (livro is null)
            return NotFound("Livro não encontrado");
        return Ok(livro);
    }

    [HttpPost]
    public ActionResult Post(Livro? livro)
    {
        if (livro is null)
            return BadRequest("livro é invalido");
        _context.Add(livro);
        _context.SaveChanges();
        return CreatedAtRoute("ObterLivro", new { livro.Id }, livro);
    }

    [HttpPut("{id:int:min(1)}")]
    public ActionResult Put(int id, Livro livro)
    {
        var l = _context.Livros.SingleOrDefault(x => x.Id == id);
        if (l is null)
            return BadRequest("livro é invalido");
        if (livro.Id != id)
            return NotFound("Os id são diferentes");
        _context.Livros.Update(livro);
        _context.SaveChanges();
        return Ok(livro);
    }

    [HttpDelete("{id:int:min(1)}")]
    public ActionResult Delete(int id)
    {
        var l = _context.Livros.SingleOrDefault(x => x.Id == id);
        if (l is null)
            return BadRequest("livro é invalido");
        _context.Livros.Remove(l);
        _context.SaveChanges();
        return Ok(l);
    }
}