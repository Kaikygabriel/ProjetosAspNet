using NotifiMe.Data;
using NotifiMe.Repository.Interface;

namespace NotifiMe.Repository;

public class UnitOfWork : IUnitOfWork
{
    private AppDbContext context;
    private IUserRepository _repositoryUser;
    private IProviderRepository _repositoryProvider;
    public UnitOfWork(AppDbContext context)
    {
        this.context = context;
    }

    public async Task CommitAsync()
    {
        await context.SaveChangesAsync();
    }

    public IUserRepository UserRepository
    {
        get
        {
            return _repositoryUser = UserRepository ?? new RepositoryUser(context);
        }
    }

    public IProviderRepository ProviderRepository
    {
        get
        {
            return _repositoryProvider = _repositoryProvider ?? new RepositoryProvider(context);
        }
    }
}