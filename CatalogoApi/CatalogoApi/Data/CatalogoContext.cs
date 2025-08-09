using CatalogoApi.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CatalogoApi.Data;
public class CatalogoContext : IdentityDbContext
{
    public CatalogoContext(DbContextOptions<CatalogoContext> options) : base(options) { }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Categoria> Categorias { get; set; }

}

