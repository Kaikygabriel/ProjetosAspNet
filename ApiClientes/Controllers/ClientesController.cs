using ApiClientes.Data;
using ApiClientes.Model;
using ApiClientes.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiClientes.Controllers;

[ApiController]
[Route("[controller]")]
public class ClientesController : ControllerBase
{

    public ClientesController(IClientesRepository context)
    {
        _context = context;
    }
    private readonly IClientesRepository _context;
    
    [HttpGet]
    public ActionResult Get()
    {
            IEnumerable<Cliente> clientes = _context.GetClientes();
            if (clientes is null)
                return NotFound();
            return Ok(clientes);
    }

     [HttpGet("{id:int:min(1)}")]
    public ActionResult<Cliente> Get(int id)
    {
            var cliente =  _context.GetCliente(id);
            if (cliente is null)
                return NotFound("Cliente não encontrado");
            return Ok(cliente);
    }

    [HttpPost]
    public ActionResult Post([FromBody]Cliente cliente)
    {
            _context.Create(cliente);
            return Created();
    }

    [HttpPut("{id:int:min(1)}")]
    public ActionResult Put(int id, Cliente cliente)
    {
  
            if (id != cliente.Id)
                return BadRequest("Id informado é diferente do id do cliente");
            _context.Update(cliente);
            return Ok(cliente);
    }

    [HttpDelete("{id:int:min(1)}")]
    public ActionResult Delete(int id)
    {
        var cliente = _context.GetCliente(id);
            if (cliente is null)
                return BadRequest("Cliente não existe");
        _context.Delete(id);
            return Ok(cliente);
    }
}