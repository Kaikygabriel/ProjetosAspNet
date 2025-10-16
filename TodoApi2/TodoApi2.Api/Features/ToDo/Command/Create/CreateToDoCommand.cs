using MediatorX.Core.Abstraction.Interfaces;

namespace TodoApi2.Api.Features.ToDo.Command.Create;

public record CreateToDoCommand(string Title) : IRequest<bool>; 
