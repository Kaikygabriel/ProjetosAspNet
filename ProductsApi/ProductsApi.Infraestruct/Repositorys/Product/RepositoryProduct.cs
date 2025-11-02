using ProductsApi.Domain.BackOffice.Interfaces;
using ProductsApi.Domain.BackOffice.Interfaces.Products;
using ProductsApi.Infraestruct.Data.Context;

namespace ProductsApi.Infraestruct.Repositorys.Product;

public class RepositoryProduct:Repository<Domain.BackOffice.Entities.Product>,IRepositoryProduct
{
    public RepositoryProduct(AppDbContext context) : base(context)
    {
    }
}