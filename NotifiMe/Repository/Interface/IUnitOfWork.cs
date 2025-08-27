namespace NotifiMe.Repository.Interface;

public interface IUnitOfWork
{
    Task CommitAsync();
    public IUserRepository UserRepository { get; }
    public IProviderRepository ProviderRepository { get; }
}