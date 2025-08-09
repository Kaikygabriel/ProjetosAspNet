namespace ApiCompras.Repository.Interface;


public interface IUnitOfWork
{
    Task CommitAsync();
    public IVendaRepository VendaRepository { get; }
}