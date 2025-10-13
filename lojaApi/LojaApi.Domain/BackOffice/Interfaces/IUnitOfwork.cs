using LojaApi.Domain.BackOffice.Interfaces.Category;
using LojaApi.Domain.BackOffice.Interfaces.Product;

namespace LojaApi.Domain.BackOffice.Interfaces;

public interface IUnitOfWork
{
     IRepositoryCategory RepositoryCategory{ get; }
     IRepositoryProduct RepositoryProduct { get; }

     Task CommitAsync();
}