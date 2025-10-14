using System.Data.SqlClient;
using Dapper;
using TodoApi2.Api.Entity;
using TodoApi2.Api.Extesions;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.MapGet("/teste", async () =>
{
    using var connection = new SqlConnection(connectionString);
    var query = await connection.QueryAsync<Tarefa>("select * from [Tarefas]");
    return query is IEnumerable<Tarefa> ? Results.Ok(query) : Results.NotFound();
});

app.UseHttpsRedirection();

app.Run();