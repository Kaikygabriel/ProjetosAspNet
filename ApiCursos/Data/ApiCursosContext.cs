using ApiCursos.Model;
using Microsoft.EntityFrameworkCore;

namespace ApiCursos.Data;

public class ApiCursosContext : DbContext
{
    public ApiCursosContext(DbContextOptions<ApiCursosContext>options):base(options)
    {
        
    }

    public DbSet<Curso> Cursos { get; set; }
}