using MediatorX.Core.Abstraction.Interfaces;
using ToDoApi.Entities;

namespace ToDoApi.Services.ToDos.Commands.Create;

public class CreateToDoRequest : IRequest<bool>
{
    public ToDo ToDo { get; set; }
    
}