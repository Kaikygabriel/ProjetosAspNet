using ApiCompras.Repository.Interface;

namespace ApiCompras.Repository;

public class UnitOfWork : IUnitOfWork
{

    public UnitOfWork(VendaContext context)
    {
        Context = context;
    }
    public VendaContext Context;
    private IVendaRepository _vendaRepository;
    public IVendaRepository VendaRepository
    {
        get
        {
            return _vendaRepository = _vendaRepository ?? new VendaRepository(Context);      
        }
    }

    public async Task CommitAsync()
    {
        await Context.SaveChangesAsync();
    }
}