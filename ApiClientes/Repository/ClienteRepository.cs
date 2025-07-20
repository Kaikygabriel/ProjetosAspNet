using ApiClientes.Data;
using ApiClientes.Model;
using Microsoft.EntityFrameworkCore;

namespace ApiClientes.Repository.Interfaces;

public class ClienteRepository : Repository<Cliente>,IClientesRepository
{
    private readonly ClienteContext context;

    public ClienteRepository(ClienteContext context) : base(context)
    {
    }
}
