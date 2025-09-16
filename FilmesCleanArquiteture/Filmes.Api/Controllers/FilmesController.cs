using Filmes.Domain.Entities;
using Filmes.Domain.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Filmes.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class FilmesController : ControllerBase
{
    private readonly IUnitOfWork _uow;

    public FilmesController(IUnitOfWork uow)
    {
        _uow = uow;
    }

    [HttpGet]
    public async Task<ActionResult> GetAsync()
    {
        var filmes = await _uow.RepositoryFilme.GetAll(new CancellationToken());
        if (filmes is null)
            return BadRequest();
        return Ok(filmes);
    }
}