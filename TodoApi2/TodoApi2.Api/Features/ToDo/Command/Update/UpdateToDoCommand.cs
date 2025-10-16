using MediatorX.Core.Abstraction.Interfaces;

namespace TodoApi2.Api.Features.ToDo.Command.Update;

public record UpdateToDoCommand(int Id ,string Title) : IRequest<bool>;