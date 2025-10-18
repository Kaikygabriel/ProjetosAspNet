using Microsoft.EntityFrameworkCore;
using ProductsApi.Domain.BackOffice.Entitys;
using ProductsApi.Infraestruct.Data.Mapping;

namespace ProductsApi.Infraestruct.Data.Context;

public class AppDbContext(DbContextOptions<AppDbContext>options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Product>Products { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UsersMapping());
        modelBuilder.ApplyConfiguration(new ProductsMapping());
    }
}