using System.Data.SqlClient;
using Dapper;
using MediatorX.Core.Abstraction.Interfaces;
using TodoApi2.Api.Data;
using TodoApi2.Api.Features.ToDo.Command.Create;
using TodoApi2.Api.Features.ToDo.Command.Delete;
using TodoApi2.Api.Features.ToDo.Command.Update;

namespace TodoApi2.Api.Features.ToDo.Command;

public class ToDoCommandHandler :
    IHandler<CreateToDoCommand,bool>,
    IHandler<UpdateToDoCommand,bool>,
    IHandler<DeleteToDoCommand,bool>
{

    private AppDbContext.GetConnection _connection;

    public ToDoCommandHandler(AppDbContext.GetConnection connection)
    {
        _connection = connection;
    }

    public async Task<bool> HandleAsync(CreateToDoCommand request, CancellationToken cancellationToken = new CancellationToken())
    {
        try
        {
            using var connection =await _connection();
            var executing = await connection.ExecuteAsync
            ("INSERT INTO [TAREFAS] VALUES (@title)", new
            {
                title = request.Title
            });
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public async Task<bool> HandleAsync(UpdateToDoCommand request, CancellationToken cancellationToken = new CancellationToken())
    {
        try
        {
            using var connection = await _connection();
            var executing = await connection.ExecuteAsync
            ("UPDATE FROM [TAREFAS] SET Title = @title WHERE Id = @id;"
             , new
            {
                title = request.Title,
                id = request.Id    
            });
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public async Task<bool> HandleAsync(DeleteToDoCommand request, CancellationToken cancellationToken = new CancellationToken())
    {
            try
            {
                using var connection = await _connection();
                var executing = await connection.ExecuteAsync
                ("DELETE FROM [TAREFAS] WHERE [TAREFAS].[ID] = @id", new
                {
                    id = request.Id    
                });
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
    }
}