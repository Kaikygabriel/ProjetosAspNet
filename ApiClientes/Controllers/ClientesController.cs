using ApiClientes.Data;
using ApiClientes.Model;
using ApiClientes.Repository;
using ApiClientes.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiClientes.Controllers;

[ApiController]
[Route("[controller]")]
public class ClientesController : ControllerBase
{
    public ClientesController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    private readonly IUnitOfWork _unitOfWork;
    [HttpGet]
    public ActionResult Get()
    {
        IEnumerable<Cliente> clientes = _unitOfWork.RepositoryCliente.GetAll();
        if (clientes is null)
            return NotFound();
        return Ok(clientes);
    }

     [HttpGet("{id:int:min(1)}",Name = "obter")]
    public ActionResult<Cliente> Get(int id)
    {
        var cliente =  _unitOfWork.RepositoryCliente.GetById(c=>c.Id==id);
        if (cliente is null)
            return NotFound("Cliente não encontrado");
        return Ok(cliente);
    }

    [HttpPost]
    public ActionResult Post([FromBody]Cliente cliente)
    {
        if (cliente is null)
            return NotFound("Cliente is null");
        _unitOfWork.RepositoryCliente.Create(cliente);
        _unitOfWork.Commit();
        return CreatedAtAction("obter",new {cliente.Id},cliente);
    }

    [HttpPut("{id:int:min(1)}")]
    public ActionResult Put(int id, Cliente cliente)
    {
        if (cliente is null)
            return NotFound("Cliente is null");
        if (id != cliente.Id) 
            return BadRequest("Id informado é diferente do id do cliente");
        _unitOfWork.RepositoryCliente.Update(cliente);
        _unitOfWork.Commit();
        return Ok(cliente);
    }

    [HttpDelete("{id:int:min(1)}")]
    public ActionResult Delete(int id)
    {
        var cliente = _unitOfWork.RepositoryCliente.GetById(c=>c.Id==id);
        if (cliente is null)
            return BadRequest("Cliente não existe");
        _unitOfWork.RepositoryCliente.Delete(cliente);
        _unitOfWork.Commit();
        return Ok(cliente);
    }
}