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

        public async Task<PagedList<Produto>> GetAllProductAsync(ProdutosPagination pagination)
        {
            var produtos = await GetAllAsync();
            var produtosOrdenados= PagedList<Produto>.ToPagedList(produtos,pagination.PageNumber,pagination.PageSize);
            return produtosOrdenados;
        }

        public async Task<PagedList<Produto>> GetProdutosFiltroPrecoAsync(ProdutosFiltroPreco produtosFiltro)
        {
            var produtos = await GetAllAsync();
            if(produtosFiltro.Preco.HasValue && !string.IsNullOrEmpty(produtosFiltro.PrecoCriterio))
            {
                if (produtosFiltro.PrecoCriterio.Equals("maior", StringComparison.OrdinalIgnoreCase))
                    produtos = produtos.Where(x => x.Preco > produtosFiltro.Preco).OrderBy(x => x.Preco);
                else if (produtosFiltro.PrecoCriterio.Equals("menor", StringComparison.OrdinalIgnoreCase))
                    produtos = produtos.Where(x => x.Preco < produtosFiltro.Preco).OrderBy(x => x.Preco);
                else if (produtosFiltro.PrecoCriterio.Equals("igual", StringComparison.OrdinalIgnoreCase))
                    produtos = produtos.Where(x => x.Preco == produtosFiltro.Preco).OrderBy(x => x.Preco);
            }
            return PagedList<Produto>.ToPagedList(produtos, produtosFiltro.PageNumber,produtosFiltro.PageSize);
        }
    }
}
