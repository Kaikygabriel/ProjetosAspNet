using System.Data.SqlClient;
using Dapper;
using MediatorX.Core.Abstraction.Interfaces;
using MediatorX.Core.DependencyInjection;
using TodoApi2.Api.Data;
using TodoApi2.Api.Entity;
using TodoApi2.Api.Extesions;
using TodoApi2.Api.Features.ToDo;
using TodoApi2.Api.Features.ToDo.Command;

var builder = WebApplication.CreateBuilder(args);

builder.AddPersistence();
builder.Services.AddMediator(typeof(Program).Assembly);
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

app.MapTarefasEndPoint();

app.UseHttpsRedirection();

app.Run();