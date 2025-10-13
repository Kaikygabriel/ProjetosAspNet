using System.Reflection;
using MediatorX.Core.Abstraction.Interfaces;
using MediatorX.Core.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ToDoApi.Data;
using ToDoApi.Entities;
using ToDoApi.Repository;
using ToDoApi.Repository.Interface;
using ToDoApi.Services.ToDos.Commands.Create;

var builder = WebApplication.CreateBuilder(args);

var connection = builder.Configuration.GetConnectionString("DefaulsConnection");

builder.Services.AddMediator(typeof(Program).Assembly);
builder.Services.AddScoped<IRepositoryToDo, RepositoryTodo>();
builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connection,ServerVersion.AutoDetect(connection)));

var app = builder.Build();

app.MapGet("/", () => "Ola Mundo!");

app.MapGet("/ToDo", async (IUnitOfWork unit) 
    => await unit.RepositoryToDo.GetAll());

app.MapPost("/ToDo", async (ToDo todo, IMediator Mediator) =>
{
    CreateToDoRequest todoCreate = new() { ToDo = todo };
    var response = await Mediator.SendAsync(todoCreate);
    return (response) ? Results.Created() : Results.NotFound();
});

app.Run(); 