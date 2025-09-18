using Filmes.Application.Services.Interfaces;
using Filmes.Domain.Entities;
using Filmes.Domain.Interfaces;
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
        var filmes = await _uow.GetAll(new CancellationToken());
        if (filmes is null)
            return BadRequest();
        return Ok(filmes);
    } 
    [HttpGet("{id:int:min(1)}")]
    public async Task<ActionResult> GetAsync(int id)
    {
        var filmes = await _uow.GetByPredicate(x=>x.Id ==id,new CancellationToken());
        if (filmes is null)
            return BadRequest();
        return Ok(filmes);
    }
}