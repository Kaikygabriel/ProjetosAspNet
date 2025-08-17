using APiCursos.Model;
using APiCursos.Model.DTO;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace APiCursos.Data;

public class ApiCursoContext : IdentityDbContext
{
    public ApiCursoContext(DbContextOptions<ApiCursoContext>options):base(options)
    {
        
    }
    public DbSet<Curso>Cursos { get; set; }
}