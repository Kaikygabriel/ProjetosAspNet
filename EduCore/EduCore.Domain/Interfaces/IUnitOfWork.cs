namespace EduCore.Domain.Interfaces;

public interface IUnitOfWork
{
    public IRepositoryProvider RepositoryProvider  { get; }
    public IRepositoryCourse RepositoryCourse  { get; }
    public IRepositoryUser RepositoryUser  { get; }
    public IRepositoryStudent RepositoryStudent  { get; }
    Task CommitAsync();
}