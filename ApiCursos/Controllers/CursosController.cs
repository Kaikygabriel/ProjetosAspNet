using APiCursos.Model;
using ApiCursos.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiCursos.Controllers;

[ApiController]
[Route("[controller]")]
public class CursosController : ControllerBase
{
    
    public CursosController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    private readonly IUnitOfWork _unitOfWork;

    [HttpGet]
    public ActionResult<IEnumerable<Curso>> Get()
    {
        IEnumerable<Curso> cursos = _unitOfWork.RepositoryCurso.GetAll();
        if (cursos is null)
            return NotFound("List esta vazia");
        return Ok(cursos);
    }

    [HttpGet("{id:int:min(1)}", Name = "GetById")]
    public ActionResult<Curso> Get(int id)
    {
        var curso = _unitOfWork.RepositoryCurso.GetByID(x => x.Id == id);
        if (curso is null)
            return NotFound("Curso não encontrado...");
        return Ok(curso);
    }

    [HttpPost]
    public ActionResult<Curso> Post(Curso curso)
    {
        if (curso is null)
            return BadRequest("Curso recebido é nulo");
        _unitOfWork.RepositoryCurso.Create(curso);
        _unitOfWork.Commit();
        return CreatedAtRoute("GetByID", new { curso.Id }, curso);
    }

    [HttpPut("{id:int:min(1)}")]
    public ActionResult<Curso> Put(int id, Curso curso)
    {
        if (curso is null)
            return BadRequest("Curso recebido é nulo");
        if (curso.Id != id)
            return NotFound("Id do curso é diferente de id informado");
        _unitOfWork.RepositoryCurso.Update(curso);
        _unitOfWork.Commit();
        return Ok(curso);
    }
    [HttpDelete("{id:int:min(1)}")]
    public ActionResult<Curso> Delete(int id)
    {
        var curso = _unitOfWork.RepositoryCurso.GetByID(x => x.Id == id);
        if (curso is null)
            return BadRequest("Curso recebido é nulo");
        _unitOfWork.RepositoryCurso.Delete(curso);
        _unitOfWork.Commit();
        return Ok(curso);
    }

}