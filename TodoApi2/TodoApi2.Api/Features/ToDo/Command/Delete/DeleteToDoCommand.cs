using MediatorX.Core.Abstraction.Interfaces;

namespace TodoApi2.Api.Features.ToDo.Command.Delete;

public record DeleteToDoCommand(int Id ) : IRequest<bool>;