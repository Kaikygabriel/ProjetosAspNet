using ToDoApi.Data;
using ToDoApi.Repository.Interface;

namespace ToDoApi.Repository;

public class UnitOfWork : IUnitOfWork
{
    private RepositoryTodo _repository;
    private readonly AppDbContext context;

    public UnitOfWork(AppDbContext context)
    {
        this.context = context;
    }

    public async Task CommitAsync()
    {
        await context.SaveChangesAsync();
    }

    public IRepositoryToDo RepositoryToDo
    {
        get
        {
            return _repository = _repository ?? new RepositoryTodo(context);
        }
    }
}