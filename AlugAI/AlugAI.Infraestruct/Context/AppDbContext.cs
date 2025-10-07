using AlugAI.Domain.Entities;
using AlugAI.Domain.ObjectValues;
using Microsoft.EntityFrameworkCore;

namespace AlugAI.Infraestruct.Context;

public class AppDbContext(DbContextOptions<AppDbContext>options) : DbContext(options)
{
    public DbSet<Provider> Provider{ get; set; }
    public DbSet<Consumer>Consumers { get; set; }
}