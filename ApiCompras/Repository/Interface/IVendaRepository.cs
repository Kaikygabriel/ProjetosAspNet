using ApiCompras.Model;
using ApiCompras.Pagination;
using APICOMPRAS.Pagination;

namespace ApiCompras.Repository.Interface;

public interface IVendaRepository : IRepository<Venda>
{
    Task<PagedList<Venda>> GetAllPaginationAsync(ClientePagination pagination);
}