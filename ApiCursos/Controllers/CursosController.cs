using ApiCursos.Data;
using ApiCursos.Model;
using Microsoft.AspNetCore.Mvc;

namespace ApiCursos.Controllers;

[ApiController]
[Route("[controller]")]
public class CursosController :  ControllerBase
{
    public CursosController(ApiCursosContext context)
    {
        this.context = context;
    }

    private readonly ApiCursosContext context;

    [HttpGet]
    public ActionResult Get()
    {
        IEnumerable<Curso> cursos = context.Cursos.ToList();
        if (cursos is null)
            return NotFound("Lista de cursos esta vazia");
        return Ok(cursos);
    }

    [HttpGet("{id:int}")]
    public ActionResult Get(int id)
    {
        var curso = context.Cursos.FirstOrDefault(x => x.Id == id);
        if (curso is null)
            return NotFound("curso não encontrado");
        
        return Ok(curso);
    }
    
    [HttpPost]
    public ActionResult Post(Curso curso)
    {
        if (curso is null)
            return NotFound("Curso é nulo");
        context.Cursos.Add(curso);
        context.SaveChanges();
        return CreatedAtAction("ObterCurso", new { curso.Id }, curso);
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Curso curso)
    {
        if (curso.Id != id)
            return BadRequest("Id é diferente de id do curso");
        if (curso is null)
            return NotFound();
        context.Update(curso);
        context.SaveChanges();
        return Ok(curso);
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var curso = context.Cursos.FirstOrDefault(x => x.Id == id);
        if (curso is null)
            return NotFound("curso não encontrado");
        context.Remove(curso);
        context.SaveChanges();
        return Ok(curso);
    }
}