using Filmes.Domain.Entities;
using Filmes.Infraestruture.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Filmes.Infraestruture.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<User>(options)
{
    public DbSet<Filme>Filmes { get; set; }
}