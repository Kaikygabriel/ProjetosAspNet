using LojaApi.Domain.BackOffice.Entitys;
using Microsoft.EntityFrameworkCore;

namespace LojaApi.Infraestruct.Context;

public class AppDbContext(DbContextOptions<AppDbContext>options) : DbContext(options)
{
    public DbSet<Product>Products { get; set; }
    public DbSet<Category>Categories { get; set; }
}