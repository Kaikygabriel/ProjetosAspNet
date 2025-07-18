using System.Runtime.CompilerServices;
using ApiCursos.Data;
using ApiCursos.Model;
using ApiCursos.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCursos.Controllers;

[ApiController]
[Route("[controller]")]
public class CursosController :  ControllerBase
{
    private readonly ICursoRepository _repository;
    public CursosController(ICursoRepository repository)
    {
        _repository = repository;
    }


    [HttpGet]
    public ActionResult Get()
    {
        IEnumerable<Curso> cursos = _repository.GetCursos();
        if (cursos is null)
            return NotFound("Lista de cursos esta vazia");
        return Ok(cursos);
    }

    [HttpGet("{id:int:min(1)}")]
    public ActionResult Get(int id)
    {
        var curso = _repository.GetCurso(id);
        if (curso is null)
            return NotFound("curso não encontrado");
        
        return Ok(curso);
    }
    
    [HttpPost]
    public ActionResult Post(Curso curso)
    {
        if (curso is null)
            return NotFound("Curso é nulo");
        _repository.Create(curso);
        return Ok(curso);
    }

    [HttpPut("{id:int:min(1)}")]
    public ActionResult Put(int id, Curso curso)
    {
        if (curso.Id != id)
            return BadRequest("Id é diferente de id do curso");
        if (curso is null)
            return NotFound();
        _repository.Update(curso);
        return Ok(curso);
    }

    [HttpDelete("{id:int:min(1)}")]
    public ActionResult Delete(int id)
    {
        var curso = _repository.GetCurso(id);
        if (curso is null)
            return NotFound("curso não encontrado");
        _repository.Delete(id);
        return Ok(curso);
    }
}