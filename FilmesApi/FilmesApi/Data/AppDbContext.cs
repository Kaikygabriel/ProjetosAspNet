using FilmesApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FilmesApi.Data;

public class AppDbContext(DbContextOptions<AppDbContext>options) : IdentityDbContext<User>(options)
{
    public DbSet<Filme>Filmes { get; set; }   
}