using ApiClientes.Data;
using ApiClientes.Model;
using Microsoft.AspNetCore.Mvc;

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
    public ActionResult Get()
    {
        IEnumerable<Cliente> clientes = _context.Clientes.ToList();
        if (clientes is null)
            return NotFound();
        return Ok(clientes);
    }

     [HttpGet("{id:int}",Name = "obter")]
    public ActionResult<Cliente> Get(int id)
    {
        var cliente = _context.Clientes.SingleOrDefault(x => x.Id == id);
        if (cliente is null)
            return NotFound("Cliente não encontrado");
        return cliente;
    }

    [HttpPost]
    public ActionResult Post(Cliente cliente)
    {
        if (cliente is null)
            return BadRequest("Cliente nulo");
        _context.Clientes.Add(cliente);
        _context.SaveChanges();
        return RedirectToRoute("obter", new { cliente.Id });
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Cliente cliente)
    {
        if (id != cliente.Id)
            return BadRequest("Id informado é diferente do id do cliente");
        _context.Update(cliente);
        _context.SaveChanges();
        return Ok(cliente);
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var cliente = _context.Clientes.SingleOrDefault(x => x.Id == id);
        if (cliente is null)
            return NotFound("Cliente não existe");
        _context.Clientes.Remove(cliente);
        _context.SaveChanges();
        return Ok(cliente);
    }
}