using ApiCompras.Model;
using ApiCompras.Repository.Interface;

namespace ApiCompras.Repository;

public class VendaRepository : Repository<Venda>, IVendaRepository
{
    public VendaRepository(VendaContext context) : base(context)
    {
    }
}