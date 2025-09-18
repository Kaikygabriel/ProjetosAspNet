using Catalogo.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Catalogo.API.Controllers;

[Route("[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly IUnitOfWork _uwf;

    public ProdutosController(IUnitOfWork unit)
    {
        _uwf = unit;
    }

    [HttpGet("{id:int:min(1)}")]
    public async Task<ActionResult> GetAsync(int id)
    {
        var produto = await _uwf.RepositoryProduto.GetByPredicate(x => x.Id == id);
        if (produto is null)
            return NotFound();
        return Ok(produto);
    }
    [HttpGet]
    public async Task<ActionResult> GetAsync()
    {
        var produtos = await _uwf.RepositoryProduto.GetAllAsync();
        if (produtos is null)
            return NotFound();
        return Ok(produtos);
    }

}