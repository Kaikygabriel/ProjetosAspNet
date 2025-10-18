using ProductsApi.Domain.BackOffice.Interfaces;
using ProductsApi.Domain.BackOffice.Interfaces.Products;
using ProductsApi.Domain.BackOffice.Interfaces.Users;
using ProductsApi.Infraestruct.Data.Context;
using ProductsApi.Infraestruct.Repositorys.Product;
using ProductsApi.Infraestruct.Repositorys.User;

namespace ProductsApi.Infraestruct.Repositorys;

public class UniOfWork : IUnitOfWork
{
    private  RepositoryProduct _repositoryProduct;
    private RepositoryUser _repositoryUser;

    private readonly AppDbContext _context;

    public UniOfWork(AppDbContext context)
    {
        _context = context;
    }

    public IRepositoryProduct RepositoryProduct
    {
        get
        {
            return _repositoryProduct = _repositoryProduct ?? new RepositoryProduct(_context);
        }
    }

    public IRepositoryUser RepositoryUser
    {
        get
        {
            return _repositoryUser = _repositoryUser ?? new RepositoryUser(_context);
        }
    }

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task RollBackAsync()
    {
        await Task.Delay(0);
    }
}