using ApiConsultasMedicas.Model;
using Microsoft.EntityFrameworkCore;

namespace ApiConsultasMedicas.Data;

public class ApiConsultaContext : DbContext
{
    public ApiConsultaContext(DbContextOptions<ApiConsultaContext>options):base(options)
    {
        
    }
    public DbSet<Consulta> Consultas { get; set; }
}