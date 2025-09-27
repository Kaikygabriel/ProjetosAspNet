namespace Biblioteca.Domain.Interfaces;

public interface IUnitOfWork
{
    public IRepositoryUser RepositoryUser { get;}
    public IRepositoryBook RepositoryBook { get;}
    Task CommitAsync();
    
}