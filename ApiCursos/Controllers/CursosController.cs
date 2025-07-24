using ApiCursos.ExtesionMethods;
using APiCursos.Filter;
using APiCursos.Model;
using APiCursos.Model.DTO;
using ApiCursos.Repository.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ApiCursos.Controllers;

[ApiController]
[Route("[controller]")]
public class CursosController : ControllerBase
{
    
    public CursosController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    [HttpGet]
    public ActionResult<IEnumerable<CursoDTO>> Get()
    {
        
        IEnumerable<Curso> cursos = _unitOfWork.RepositoryCurso.GetAll();
        if (cursos is null)
            return NotFound("List esta vazia");
        var cursosDto = _mapper.Map<IEnumerable<CursoDTO>>(cursos);
        return Ok(cursosDto);
    }

    [HttpGet("{id:int:min(1)}", Name = "GetById")]
    public ActionResult<CursoDTO> Get(int id)
    {
        var curso = _unitOfWork.RepositoryCurso.GetByID(x => x.Id == id);
        if (curso is null)
            return NotFound("Curso não encontrado...");
        var cursoDto = _mapper.Map<CursoDTO>(curso);
        return Ok(cursoDto);
    }

    [HttpPost]
    public ActionResult<CursoDTO> Post(CursoDTO cursoDTO)
    {
        var curso = _mapper.Map<Curso>(cursoDTO);
        if (curso is null)
            return BadRequest("Curso recebido é nulo");
        _unitOfWork.RepositoryCurso.Create(curso);
        _unitOfWork.Commit();
        return CreatedAtRoute("GetByID", new { cursoDTO.Id }, cursoDTO);
    }

    [HttpPut("{id:int:min(1)}")]
    public ActionResult<CursoDTO> Put(int id, CursoDTO cursoDTo)
    {
        var curso = _mapper.Map<Curso>(cursoDTo);
        if (curso is null)
            return BadRequest("Curso recebido é nulo");
        if (curso.Id != id)
            return NotFound("Id do curso é diferente de id informado");
        _unitOfWork.RepositoryCurso.Update(curso);
        _unitOfWork.Commit();
        return Ok(cursoDTo);
    }
    [HttpDelete("{id:int:min(1)}")]
    public ActionResult<CursoDTO> Delete(int id)
    {
        var curso = _unitOfWork.RepositoryCurso.GetByID(x => x.Id == id);
        if (curso is null)
            return BadRequest("Curso recebido é nulo");
        _unitOfWork.RepositoryCurso.Delete(curso);
        _unitOfWork.Commit();
        var cursoDto = _mapper.Map<CursoDTO>(curso);
        return Ok(cursoDto);
    }

}