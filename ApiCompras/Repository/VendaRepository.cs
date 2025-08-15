using ApiCompras.Model;
using ApiCompras.Pagination;
using ApiCompras.Repository.Interface;
using APICOMPRAS.Pagination;

namespace ApiCompras.Repository;

public class VendaRepository : Repository<Venda>, IVendaRepository
{
    public VendaRepository(VendaContext context) : base(context)
    {
    }

    public async Task<PagedList<Venda>> GetAllPaginationAsync(ClientePagination pagination)
    {
        var data = await GetAllAsync();
        return PagedList<Venda>.CreatedPagedList(data, pagination.PageSize,pagination.PageNumber);
    }
}