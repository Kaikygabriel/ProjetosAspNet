using ApiClientes.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ApiClientes.Data;

public class ClienteContext : IdentityDbContext
{
    public ClienteContext(DbContextOptions<ClienteContext>options):base(options)
    {
        
    }

    public  DbSet<Cliente> Clientes { get; set; }
}