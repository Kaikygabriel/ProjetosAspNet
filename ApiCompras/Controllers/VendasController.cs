using ApiCompras.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCompras.Controllers;

[ApiController]
[Route("[controller]")]
public class VendasController : ControllerBase
{
    public VendasController(VendaContext context)
    {
        _context = context;
    }
    
    private readonly VendaContext _context;

    [HttpGet]
    public async Task<ActionResult> GetAsync()
    {
      
        IEnumerable<Venda> vendas = await _context.Vendas.AsNoTracking().Take(10).ToListAsync();
        return Ok(vendas);
    }

    [HttpGet("{Id:int:min(1)}")]
    public async Task<ActionResult> GetAsync(int Id)
    {
        var venda = await _context.Vendas.AsNoTracking().SingleOrDefaultAsync(x => x.Id == Id);
        if (venda is null)
            return NotFound("Venda não encontrada ....");
        return Ok(venda);
    }

    [HttpPost]
    public ActionResult Post(Venda venda)
    {
        _context.Add(venda);
        _context.SaveChanges();
        return Created();
    }

    [HttpPut("{Id:int:min(1)}")]
    public ActionResult Put(int id, Venda venda)
    {
        if (venda.Id != id)
            return BadRequest("Error id é diferente do id passado no corpo da requisição ...");
        _context.Update(venda);
        _context.SaveChanges();
        return Ok(venda);
    }

    [HttpDelete("{id:int:min(1)}")]
    public ActionResult Delete(int id)
    {
        var venda =  _context.Vendas.AsNoTracking().SingleOrDefault(x => x.Id == id);
        if (venda is null)
            return NotFound("Venda não encontrada ....");
        _context.Remove(venda);
        _context.SaveChanges();  
        return Ok(venda);
    }
}