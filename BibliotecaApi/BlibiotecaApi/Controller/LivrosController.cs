using BlibiotecaApi.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlibiotecaApi.Controller;

[ApiController]
[Route("[controller]")]
public class LivrosController : ControllerBase
{
     private readonly IUnitOfWork _unit;

     public LivrosController(IUnitOfWork unit)
     {
          _unit = unit;
     }

     [HttpGet]
     [Authorize]
     public async Task<ActionResult> GetAllFilmes()
     {
          return Ok(await _unit.blibiotecaRepository.GetAllAsync());
     }
}