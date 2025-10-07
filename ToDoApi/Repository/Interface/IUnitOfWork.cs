namespace ToDoApi.Repository.Interface;

public interface IUnitOfWork
{
    Task CommitAsync();
    public IRepositoryToDo RepositoryToDo { get; }
}