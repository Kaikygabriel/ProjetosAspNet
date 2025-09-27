using Filmes.Application.DTOS;
using Filmes.Application.Interfaces;
using Filmes.Application.Services.Interfaces;
using Filmes.Domain.Entities;
using Filmes.Domain.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Filmes.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class FilmesController : ControllerBase
{
    private readonly IFilmeServiceRepository _uow;

    public FilmesController(IFilmeServiceRepository uow)
    {
        _uow = uow;
    }
    
    [HttpGet]
    public async Task<ActionResult> GetAsync()
    {
        var filmes = await _uow.GetAll();
        if (filmes is null)
            return BadRequest();
        return Ok(filmes.Adapt<IEnumerable<FilmeDTO>>());
    } 
    [HttpGet("{id:int:min(1)}")]
    public async Task<ActionResult> GetAsync(int id)
    {
        var filmes = await _uow.GetByPredicate(x=>x.Id ==id);
        if (filmes is null)
            return BadRequest();
        return Ok(filmes.Adapt<FilmeDTO>());
    }

    [HttpPost]
    public async Task<ActionResult> CreateAsync(CreateFilmeDTO model)
    {
        if (model is null)
            return BadRequest();
        var filmeExist = await _uow.GetByPredicate(x => x.Titulo == model.Titulo);
        if (filmeExist is not null)
            return NotFound();
        var filme = model.Adapt<Filme>();

        await _uow.Create(filme);
        return Created();
    }
}