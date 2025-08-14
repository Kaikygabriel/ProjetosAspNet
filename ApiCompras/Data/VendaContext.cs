using ApiCompras.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ApiCompras;

public class VendaContext : IdentityDbContext
{
    public VendaContext(DbContextOptions<VendaContext> options): base(options) 
    {
    }

    public DbSet<Venda>Vendas { get; set; }
}