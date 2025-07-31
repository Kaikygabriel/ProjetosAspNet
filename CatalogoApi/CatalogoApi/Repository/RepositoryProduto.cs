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

        public PagedList<Produto> GetAllProduct(ProdutosPagination pagination)
        {
            IEnumerable<Produto> produtos = _context.Produtos.OrderBy(x=>x.Nome).AsNoTracking();
            var produtosOrdenados= PagedList<Produto>.ToPagedList(produtos,pagination.PageNumber,pagination.PageSize);
            return produtosOrdenados;
        }
    }
}
