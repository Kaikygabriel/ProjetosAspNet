using System.Runtime.CompilerServices;
using MediatorX.Core.Abstraction.Interfaces;
using ToDoApi.Repository.Interface;

namespace ToDoApi.Services.ToDos.Commands.Create;

public class CreateTodoHandler(IUnitOfWork unit)  : IHandler<CreateToDoRequest,bool>
{
    public async Task<bool> HandleAsync(CreateToDoRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        try
        {
            unit.RepositoryToDo.Create(request.ToDo);
            await unit.CommitAsync();
            return true;
        }
        catch(Exception e )
        {
            return false;
        }
    }
}