using EduCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EduCore.Infraestruct.Context;

public class AppDbContext(DbContextOptions<AppDbContext>options) : DbContext(options)
{
    public DbSet<User>Users { get; set; }
    public DbSet<Course>Courses { get; set; }
    public DbSet<Provider>Providers{ get; set; }
    public DbSet<Student>Students { get; set; }
}