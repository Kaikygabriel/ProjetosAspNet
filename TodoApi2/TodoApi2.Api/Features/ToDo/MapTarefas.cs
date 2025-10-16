using Dapper;
using MediatorX.Core.Abstraction.Interfaces;
using TodoApi2.Api.Data;
using TodoApi2.Api.Entity;
using TodoApi2.Api.Features.ToDo.Command;
using TodoApi2.Api.Features.ToDo.Command.Create;
using TodoApi2.Api.Features.ToDo.Command.Delete;
using TodoApi2.Api.Features.ToDo.Command.Update;
using TodoApi2.Api.Features.ToDo.Query.GetAll;
using TodoApi2.Api.Features.ToDo.Query.GetById;

namespace TodoApi2.Api.Features.ToDo;

public static class MapTarefas
{
    public static void MapTarefasEndPoint(this WebApplication app)
    {
        app.MapGet("/tarefas", async (IMediator mediator) =>
        {
            var tarefas = await mediator.SendAsync(new GetAllTarefasQuery());
            return tarefas != null ? Results.Ok(tarefas) : Results.NotFound();
        });
        app.MapGet("/tarefas/{idParamter:int:min(1)}", async (int idParamter,IMediator mediator) =>
        {
            var tarefa = await mediator.SendAsync(new GetByIdTarefasQuery(idParamter));
            return tarefa != null ? Results.Ok(tarefa) : Results.NotFound();
        });

        app.MapPost("/tarefas", async (CreateToDoCommand command,IMediator mediator) =>
        {
            var result = await mediator.SendAsync(command);
            return result ? Results.Created() : Results.NotFound();
        });
        app.MapPut("/tarefas", async (UpdateToDoCommand command,IMediator mediator) =>
        {
            var result = await mediator.SendAsync(command);
            return result ? Results.Ok() : Results.NotFound();
        });
        app.MapDelete("/tarefas/{id:int:min(1)}", async (int id,IMediator mediator) =>
        {
            var result = await mediator.SendAsync(new DeleteToDoCommand(id));
            return result ? Results.Ok() : Results.NotFound();
        });
    }
}