using ApiConsultasMedicas.Model;
using ApiConsultasMedicas.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ApiConsultasMedicas.Data;

public class ApiConsultaContext : IdentityDbContext<User>
{
    public ApiConsultaContext(DbContextOptions<ApiConsultaContext>options):base(options)
    {
        
    }
    public DbSet<Consulta> Consultas { get; set; }
}