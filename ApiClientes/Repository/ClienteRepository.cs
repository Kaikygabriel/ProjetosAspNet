using ApiClientes.Data;
using ApiClientes.Model;
using Microsoft.EntityFrameworkCore;

namespace ApiClientes.Repository;

public class ClienteRepository : IClientesRepository
{
    private readonly ClienteContext context;

    public ClienteRepository(ClienteContext context)
    {
        this.context = context;
    }

    public Cliente Create(Cliente cliente)
    {
        if (cliente is null)
            throw new Exception("Cliente is null");
        context.Clientes.Add(cliente);
        context.SaveChanges();
        return cliente;
    }

    public Cliente Delete(int id)
    {
        var cliente = GetCliente(id);
        if (cliente is null)
            throw new Exception("Cliente is null");
        context.Clientes.Remove(cliente);
        context.SaveChanges();
        return cliente;
    }

    public Cliente? GetCliente(int id)
    {
        return context.Clientes.AsNoTracking().SingleOrDefault(x=>x.Id==id);
    }

    public IEnumerable<Cliente> GetClientes()
    {
        return context.Clientes.Take(10).AsNoTracking().ToList();
    }

    public Cliente Update(Cliente cliente)
    {
        if (cliente is null)
            throw new Exception("Cliente is null");
        context.Clientes.Update(cliente);
        context.SaveChanges();
        return cliente;
    }
}
