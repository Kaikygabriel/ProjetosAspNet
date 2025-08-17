using System.Text.Json;
using System.Text.Json.Serialization;
using ApiCursos.ExtesionMethods;
using APiCursos.Filter;
using APiCursos.Model;
using APiCursos.Model.DTO;
using APiCursos.Pagination;
using ApiCursos.Repository.Interfaces;
using Mapster;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

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
    [Authorize]
    public async Task<ActionResult<IEnumerable<CursoDTO>>> GetAsync()
    {

        IEnumerable<Curso> cursos = await _unitOfWork.RepositoryCurso.GetAllAsync();
        if (cursos is null)
            return NotFound("List esta vazia");
        var cursosDto = cursos.Adapt<IEnumerable<CursoDTO>>();
        return Ok(cursosDto);
    }

    [HttpGet("{id:int:min(1)}", Name = "GetById")]
    public async Task<ActionResult<CursoDTO>> GetAsync(int id)
    {
        var curso = await _unitOfWork.RepositoryCurso.GetByIDAsync(x => x.Id == id);
        if (curso is null)
            return NotFound("Curso não encontrado...");
        var cursoDto = curso.Adapt<CursoDTO>();
        return Ok(cursoDto);
    }

    [HttpGet("pagination")]
    public async Task<ActionResult<IEnumerable<CursoDTO>>> GetAsync([FromQuery] CursoPagination pagination)
    {
        var cursos = await _unitOfWork.RepositoryCurso.GetAllCursosAsync(pagination);
        var cursosDto = cursos?.Adapt<IEnumerable<CursoDTO>>();
        var metadata = new
        {
            cursos.HasNext,
            cursos.HasPrevius
        };
        Response.Headers.Append("x-pagination", JsonSerializer.Serialize(metadata));
        return Ok(cursosDto);
    }
    [HttpPost]
    public async Task<ActionResult<CursoDTO>> PostAsync(CursoDTO cursoDTO)
    {
        var curso = cursoDTO.Adapt<Curso>();
        if (curso is null)
            return BadRequest("Curso recebido é nulo");
        await _unitOfWork.RepositoryCurso.CreateAsync(curso);
        await _unitOfWork.CommitAsync();
        return CreatedAtRoute("GetByID", new { cursoDTO.Id }, cursoDTO);
    }
    
    [HttpPatch("{id:int:min(1)}")]
    public async Task<ActionResult<Curso>> Patch(int id, JsonPatchDocument<Curso> curso)
    {
        if (curso is null)
            return NotFound();
        var cursoID = await _unitOfWork.RepositoryCurso.GetByIDAsync(x => x.Id == id);
        curso.ApplyTo(cursoID,ModelState);
        if (!ModelState.IsValid || !TryValidateModel(cursoID))
            return NotFound();
        _unitOfWork.RepositoryCurso.Update(cursoID);
        await _unitOfWork.CommitAsync();
        return Ok(curso.Adapt<CursoDTO>());
    }
    [HttpPut("{id:int:min(1)}")]
    public async Task<ActionResult<CursoDTO>> PutAsync(int id, CursoDTO cursoDTo)
    {
        var curso = cursoDTo.Adapt<Curso>();
        if (curso is null)
            return BadRequest("Curso recebido é nulo");
        if (curso.Id != id)
            return NotFound("Id do curso é diferente de id informado");
        _unitOfWork.RepositoryCurso.Update(curso);
        await _unitOfWork.CommitAsync();
        return Ok(cursoDTo);
    }
    [HttpDelete("{id:int:min(1)}")]
    public async Task<ActionResult<CursoDTO>> DeleteAsync(int id)
    {
        var curso =await _unitOfWork.RepositoryCurso.GetByIDAsync(x => x.Id == id);
        if (curso is null)
            return BadRequest("Curso recebido é nulo");
        _unitOfWork.RepositoryCurso.Delete(curso);
        await _unitOfWork.CommitAsync();
        var cursoDto = curso.Adapt<CursoDTO>();
        return Ok(cursoDto);
    }

}