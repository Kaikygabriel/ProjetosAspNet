using AcademyPro.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AcademyPro.Data;

public class AppDbContext(DbContextOptions<AppDbContext>context)  :IdentityDbContext<User>(context)
{
    public DbSet<Enrollment>Enrollments { get; set; }
    public DbSet<Curse>Curses { get; set; }
}