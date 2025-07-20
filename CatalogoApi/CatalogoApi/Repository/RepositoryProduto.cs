using CatalogoApi.Data;
using CatalogoApi.Model;
using CatalogoApi.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace CatalogoApi.Repository
{
    public class RepositoryProduto : Repository<Produto>,IRepositoryProduto
    {
        public RepositoryProduto(CatalogoContext context) : base(context)
        {
        }
    }
}
