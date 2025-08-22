using ApiConsultasMedicas.Data;
using ApiConsultasMedicas.Repository.Interface;

namespace ApiConsultasMedicas.Repository;


public class UnitOfWork : IUnitOfWork
{
    private IConsultaRepository _consultaRepository;
    public ApiConsultaContext Context;

    public UnitOfWork(ApiConsultaContext context)
    {
        Context = context;
    }

    public IConsultaRepository consultaRepository
    {
        get
        {
            return _consultaRepository ?? new ConsultaRepository(Context!);
        }
    }

    public async Task Commit()
    {
        await Context!.SaveChangesAsync();
    }
}