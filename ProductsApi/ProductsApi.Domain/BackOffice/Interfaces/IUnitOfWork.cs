using ProductsApi.Domain.BackOffice.Interfaces.Products;
using ProductsApi.Domain.BackOffice.Interfaces.Users;

namespace ProductsApi.Domain.BackOffice.Interfaces;

public interface IUnitOfWork
{
    public IRepositoryProduct RepositoryProduct{ get;}
    public IRepositoryUser RepositoryUser { get; }
    Task CommitAsync();
    Task RollBackAsync();
}