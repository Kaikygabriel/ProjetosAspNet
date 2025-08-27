
using System.Security.Principal;
using Microsoft.EntityFrameworkCore;
using NotifiMe.Models;

namespace NotifiMe.Data;

public class AppDbContext(DbContextOptions<AppDbContext>options)  : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Provider>Providers { get; set; }
    public DbSet<Appointment>Appointments { get; set; }
}