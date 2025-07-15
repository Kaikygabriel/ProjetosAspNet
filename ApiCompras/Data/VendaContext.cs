using ApiCompras.Model;
using Microsoft.EntityFrameworkCore;

namespace ApiCompras;

public class VendaContext : DbContext
{
    public VendaContext(DbContextOptions<VendaContext> options): base(options) 
    {
    }

    public DbSet<Venda>Vendas { get; set; }
}