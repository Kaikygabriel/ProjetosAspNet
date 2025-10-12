using EduCore.Domain.Interfaces;

namespace EduCore.Test.Mocks;

public class MockUnitOfWork : IUnitOfWork
{
    public IRepositoryProvider RepositoryProvider { get; } = new MockProviderRepository();
    public IRepositoryCourse RepositoryCourse { get; }
    public IRepositoryUser RepositoryUser { get; } = new MockUserRepository();
    public IRepositoryStudent RepositoryStudent { get; } = new MockStudentRepository();
    public async Task CommitAsync()
    {
        await Task.Delay(0);
    }
}