using ApiConsultasMedicas.Data;
using ApiConsultasMedicas.Model;
using ApiConsultasMedicas.Repository.Interface;

namespace ApiConsultasMedicas.Repository;


public class ConsultaRepository : Repository<Consulta>, IConsultaRepository
{
    public ConsultaRepository(ApiConsultaContext context) : base(context)
    {
    }
}