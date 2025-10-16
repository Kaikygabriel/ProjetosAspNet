using MediatorX.Core.Abstraction.Interfaces;
using TodoApi2.Api.Entity;

namespace TodoApi2.Api.Features.ToDo.Query.GetAll;

public record GetAllTarefasQuery : IRequest<IEnumerable<Tarefa>>;