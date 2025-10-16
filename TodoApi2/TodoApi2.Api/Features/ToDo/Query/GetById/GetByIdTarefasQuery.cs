using MediatorX.Core.Abstraction.Interfaces;
using TodoApi2.Api.Entity;

namespace TodoApi2.Api.Features.ToDo.Query.GetById;

public record  GetByIdTarefasQuery(int Id) : IRequest<Tarefa>;