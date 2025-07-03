using ApiClientes.Data;
using ApiClientes.Model;

namespace ApiClientes.Services;

public class SeedingCliente
{
    private readonly ClienteContext _context;

    public SeedingCliente(ClienteContext context)
    {
        _context = context;
    }

    public void Seed()
    {
        if (_context.Clientes.Any())
        {
            return;
        }
        var c1 = new Cliente(1, "kaiky", true);
        var c2 = new Cliente(2, "gabriel", false);
        _context.AddRange(c1, c2);
        _context.SaveChanges();
    }
}