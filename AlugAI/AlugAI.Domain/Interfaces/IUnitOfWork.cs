using AlugAI.Domain.Entities;

namespace AlugAI.Domain.Interfaces;

public interface IUnitOfWork
{
    Task CommitAsync();
    IRepository<Provider> ProviderRepository { get; }
    IRepository<Consumer> ConsumerRepository { get; }
}