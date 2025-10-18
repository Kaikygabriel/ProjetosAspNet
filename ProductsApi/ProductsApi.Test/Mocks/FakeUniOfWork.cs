using ProductsApi.Domain.BackOffice.Interfaces;
using ProductsApi.Domain.BackOffice.Interfaces.Products;
using ProductsApi.Domain.BackOffice.Interfaces.Users;

namespace ProductsApi.Test.Mocks;

public class FakeUniOfWork : IUnitOfWork
{
    public IRepositoryProduct RepositoryProduct { get; } = new FakeRepositoryProduct();
    public IRepositoryUser RepositoryUser { get; }
    public async Task CommitAsync()
    {
        await Task.Delay(0);
    }

    public async Task RollBackAsync()
    {
        await Task.Delay(0);
    }
}