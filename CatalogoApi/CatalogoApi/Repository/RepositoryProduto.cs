using CatalogoApi.Data;
using CatalogoApi.Model;
using CatalogoApi.Model.Dto;
using CatalogoApi.Pagination;
using CatalogoApi.Repository.Interface;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace CatalogoApi.Repository
{
    public class RepositoryProduto : Repository<Produto>, IRepositoryProduto
    {
        public RepositoryProduto(CatalogoContext context) : base(context)
        {
        }

        public IEnumerable<Produto> GetAllProduct(ProdutosPagination pagination)
        {
            if (pagination.PageNumber == 0)
                pagination.PageNumber = 1;
            IEnumerable<Produto> produtos = _context.Produtos.AsNoTracking()
                .OrderBy(x => x.Nome)
                .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .ToList();
            return produtos;
        }
    }
}
