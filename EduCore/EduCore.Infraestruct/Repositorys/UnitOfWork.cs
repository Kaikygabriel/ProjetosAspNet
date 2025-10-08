using EduCore.Domain.Interfaces;
using EduCore.Infraestruct.Context;

namespace EduCore.Infraestruct.Repositorys;

public class UnitOfWork : IUnitOfWork
{
    private RepositoryCourse _repositoryCourse;
    private RepositoryProvider _repositoryProvider;
    private RepositoryStudent _repositoryStudent;
    private RepositoryUser _repositoryUser;
    private readonly AppDbContext context;

    public UnitOfWork()
    {
        
    }
    public UnitOfWork(AppDbContext context)
    {
        this.context = context;
    }

    public IRepositoryProvider RepositoryProvider
    {
        get
        {
            return _repositoryProvider = _repositoryProvider ?? new RepositoryProvider(context);
        }
    }

    public IRepositoryCourse RepositoryCourse
    {
        get
        {
            return _repositoryCourse = _repositoryCourse ?? new RepositoryCourse(context);
        }
    }

    public IRepositoryUser RepositoryUser
    {
        get
        {
            return _repositoryUser = _repositoryUser ?? new RepositoryUser(context);
        }
    }

    public IRepositoryStudent RepositoryStudent
    {
        get
        {
            return _repositoryStudent = _repositoryStudent ?? new RepositoryStudent(context);
        }
    }

    public async Task CommitAsync()
    {
        await context.SaveChangesAsync();
    }
}