using Biblioteca.Domain.BackOffice.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Biblioteca.Infraestructure.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
    public DbSet<Author>Authores { get; set; }
    public DbSet<User>Users { get; set; }
    public DbSet<Book>Books { get; set; }
}