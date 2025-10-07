using Microsoft.EntityFrameworkCore;
using ToDoApi.Entities;

namespace ToDoApi.Data;

public class AppDbContext(DbContextOptions<AppDbContext>options) : DbContext(options)
{
    public DbSet<ToDo>Todos { get; set; }
}