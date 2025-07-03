using ApiClientes.Model;
using Microsoft.EntityFrameworkCore;

namespace ApiClientes.Data;

public class ClienteContext : DbContext
{
    public ClienteContext(DbContextOptions<ClienteContext>options):base(options)
    {
        
    }

    public  DbSet<Cliente> Clientes { get; set; }
}