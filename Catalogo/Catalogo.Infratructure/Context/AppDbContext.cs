using Catalogo.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Catalogo.Infratructure.Context;

public class AppDbContext(DbContextOptions<AppDbContext>options) : DbContext(options)
{
    public DbSet<Produto>Produtos { get; set; }
    public DbSet<Categoria>Categorias { get; set; }
    public DbSet<User>Users { get; set; }
}