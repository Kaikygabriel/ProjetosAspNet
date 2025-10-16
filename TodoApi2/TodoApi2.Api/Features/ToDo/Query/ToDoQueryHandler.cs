using Dapper;
using MediatorX.Core.Abstraction.Interfaces;
using TodoApi2.Api.Data;
using TodoApi2.Api.Entity;
using TodoApi2.Api.Features.ToDo.Query.GetAll;
using TodoApi2.Api.Features.ToDo.Query.GetById;

namespace TodoApi2.Api.Features.ToDo.Query;

public class ToDoQueryHandler: 
    IHandler<GetAllTarefasQuery,IEnumerable<Tarefa>>,
    IHandler<GetByIdTarefasQuery,Tarefa>
{
    
    private AppDbContext.GetConnection _connection;

    public ToDoQueryHandler(AppDbContext.GetConnection connection)
    {
        _connection = connection;
    }
    
    public async Task<IEnumerable<Tarefa>> HandleAsync(GetAllTarefasQuery request,
        CancellationToken cancellationToken = default)
    {
        var consulte = await _connection();
        return await consulte.QueryAsync<Tarefa>("SELECT * FROM [TAREFAS]");
    }

    public async Task<Tarefa?> HandleAsync(GetByIdTarefasQuery request,
        CancellationToken cancellationToken = default)
    {
        var consulte = await _connection();
        var tarefabyId =  
            await consulte.QueryAsync<Tarefa>("SELECT * FROM [TAREFAS] WHERE [Id] = @id",new
        {
            id = request.Id
        });
        return tarefabyId.FirstOrDefault();
    }
}