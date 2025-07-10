using ApiClientes.Data;
using ApiClientes.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiClientes.Controllers;

[ApiController]
[Route("[controller]")]
public class ClientesController : ControllerBase
{

    public ClientesController(ClienteContext context)
    {
        _context = context;
    }
    private readonly ClienteContext _context;
    
    [HttpGet]
    public async Task<ActionResult> GetAsync()
    {
        try
        {
            IEnumerable<Cliente> clientes = await _context.Clientes.Take(10).AsNoTracking().ToListAsync();
            if (clientes is null)
                return NotFound();
            return Ok(clientes);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error no servidor durando uma requisição");
        }
    }

     [HttpGet("{id:int:min(1)}")]
    public async Task<ActionResult<Cliente>> GetAsync(int id)
    {
        try
        {
            var cliente = await _context.Clientes.SingleOrDefaultAsync(x => x.Id == id);
            if (cliente is null)
                return NotFound("Cliente não encontrado");
            return Ok(cliente);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error no servidor durando uma requisição");
        }
    }

    [HttpPost]
    public ActionResult Post([FromBody]Cliente cliente)
    {
        try
        {
            _context.Clientes.Add(cliente);
            _context.SaveChanges();
            return Created();
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error no servidor durando uma requisição");
        }
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Cliente cliente)
    {
        try
        {
            if (id != cliente.Id)
                return BadRequest("Id informado é diferente do id do cliente");
            _context.Update(cliente);
            _context.SaveChanges();
            return Ok(cliente);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error no servidor durando uma requisição");
        }
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        try
        {
            var cliente = _context.Clientes.SingleOrDefault(x => x.Id == id);
            if (cliente is null)
                return BadRequest("Cliente não existe");
            _context.Clientes.Remove(cliente);
            _context.SaveChanges();
            return Ok(cliente);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error no servidor durando uma requisição");
        }
    }
}