using DevTalk.Domain.BackOffice.Entities;
using DevTalk.Infraestruct.Data.Mapping;
using Microsoft.EntityFrameworkCore;

namespace DevTalk.Infraestruct.Data.Context;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options) 
{
    public DbSet<User>Users { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new MappingUser());
    }
}