using DevTalk.Domain.BackOffice.Interfaces;
using DevTalk.Domain.BackOffice.Interfaces.User;
using DevTalk.Infraestruct.Data.Context;
using DevTalk.Infraestruct.Repositories.User;

namespace DevTalk.Infraestruct.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private RepositoryUser _repositoryUser;
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public IRepositoryUser RepositoryUser
    {
        get
        {
            return _repositoryUser = _repositoryUser ?? new RepositoryUser(_context);
        }
    }

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }
}