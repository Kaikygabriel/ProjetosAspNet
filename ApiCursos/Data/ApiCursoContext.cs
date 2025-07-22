using APiCursos.Model;
using Microsoft.EntityFrameworkCore;

namespace APiCursos.Data;

public class ApiCursoContext : DbContext
{
    public ApiCursoContext(DbContextOptions<ApiCursoContext>options):base(options)
    {
        
    }
    public DbSet<Curso>Cursos { get; set; }
}