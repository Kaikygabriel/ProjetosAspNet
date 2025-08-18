
using FilmesApi.Models;
using FilmesApi.Models.DTO;
using FilmesApi.Pagination;
using FilmesApi.Repository;
using FilmesApi.Repository.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace FilmesApi.Controllers;

[ApiController]
[Route("[controller]")]
public class FilmesController : ControllerBase
{
    public FilmesController(IUnitOfWork unitOf)
    {
        _unitOf = unitOf;
    }

    private readonly IUnitOfWork _unitOf;

    [HttpGet]
    [Authorize]
    public ActionResult<IEnumerable<FilmesDTO>> Get()
    {
        var filmes = _unitOf.FilmeRepository.GetAll();
        var filmesdto = filmes.Adapt<IEnumerable<FilmesDTO>>();
        if (filmesdto is null)
            return NotFound("Filmes não encontrados");
        return Ok(filmesdto);
    }

    [HttpGet("pagination")]
    public ActionResult<IEnumerable<FilmesDTO>> Get([FromQuery] FilmePagination pagination)
    {
        var filmes = _unitOf.FilmeRepository.GetAllFilme(pagination);
        if (filmes is null)
            return NotFound();
        var dadosHeader = new
        {
            filmes.PageNumber, filmes.PageSize,
            filmes.TotalPage, filmes.HasNext, filmes.HasPrevius, filmes.TotalCount
        };
        Response.Headers.Append("pagination", JsonSerializer.Serialize(dadosHeader));
        var filmesDto = filmes.Adapt<IEnumerable<FilmesDTO>>(); 
        return Ok(filmesDto);
    }
    [HttpGet("{id:int:min(1)}",Name = "GetByID")]
    public ActionResult<FilmesDTO> Get(int id)
    {
        var filme = _unitOf.FilmeRepository.GetById(x => x.Id == id);
        if (filme is null)
         return NotFound("Filme não encontrado...");
        var filmeDTO = filme.Adapt<FilmesDTO>();
        return Ok(filme);
    }

    [HttpPost]
    public ActionResult<FilmesDTO> Post(FilmesDTO filmedto)
    {
        if (filmedto is null)
            return BadRequest();
        var filme = filmedto.Adapt<Filme>();
        _unitOf.FilmeRepository.Created(filme);
        _unitOf.Commit();
        return  CreatedAtRoute("GetByID", new { filmedto.Id }, filmedto);
    }

    [HttpPatch("{id:int:min(1)}")]
    public ActionResult<Filme> Patch(int id, JsonPatchDocument<Filme> filmeRequest)
    {
        if (filmeRequest is null)
            return BadRequest();
        var filmeById = _unitOf.FilmeRepository.GetById(x=>x.Id==id);
        if (filmeById is null)
            return NotFound();
        filmeRequest.ApplyTo(filmeById,ModelState);
        if (!ModelState.IsValid || !TryValidateModel(filmeById))
            return NotFound(ModelState);
        var filmeAtualizado=_unitOf.FilmeRepository.Update(filmeById);
        _unitOf.Commit();
        return Ok(filmeAtualizado);
    }

    [HttpPut("{id:int:min(1)}")]
    public ActionResult<FilmesDTO> Put(int id, FilmesDTO filmesDto)
    {
        if (id != filmesDto.Id)
            return BadRequest();
        if (filmesDto is null)
            return BadRequest();
        var filme = filmesDto.Adapt<Filme>();
        var filmeAtualizado=_unitOf.FilmeRepository.Update(filme);
        _unitOf.Commit();
        return Ok(filmeAtualizado);
    }

    [HttpDelete("{id:int:min(1)}")]
    public ActionResult<FilmesDTO> Delete(int id)
    {
        var filmeById = _unitOf.FilmeRepository.GetById(x=>x.Id==id);
        if (filmeById is null)
            return BadRequest();
        _unitOf.FilmeRepository.Delete(filmeById);
        _unitOf.Commit();
        var filmeDto = filmeById.Adapt<FilmesDTO>();
        return Ok(filmeDto);
    }
}