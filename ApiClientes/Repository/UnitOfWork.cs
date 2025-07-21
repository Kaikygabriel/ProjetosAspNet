using ApiClientes.Data;

namespace ApiClientes.Repository.Interfaces;

public class UnitOfWork : IUnitOfWork
{
    public UnitOfWork(IClientesRepository clientesRepository)
    {
        _ClientesRepository = clientesRepository;
    }

    public ClienteContext context;
    private IClientesRepository? _ClientesRepository;

    public IClientesRepository RepositoryCliente
    {
        get
        {
            return _ClientesRepository = _ClientesRepository ?? new ClienteRepository(context);
        }
    }
    public void Commit()
    {
        context.SaveChanges();
    }
    public void Dispose()
    {
        context.Dispose();
    }
}