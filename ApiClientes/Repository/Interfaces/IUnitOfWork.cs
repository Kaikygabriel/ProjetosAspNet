namespace ApiClientes.Repository.Interfaces;

public interface IUnitOfWork
{
    IClientesRepository RepositoryCliente { get; }
    void Commit();
}