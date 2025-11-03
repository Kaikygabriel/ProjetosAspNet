using DevTalk.Application.UseCases.User.Command.Create;
using DevTalk.Application.UseCases.User.Query.GetById;
using DevTalk.Domain.BackOffice.Entities;
using DevTalk.Infraestruct.Data.Context;
using DevTalk.Infraestruct.DependencyInjection;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(x =>
    x.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddMediatR(x=>
    x.RegisterServicesFromAssembly(typeof(GetByNameHandler).Assembly));

builder.Services.AddDependencyInjection(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.MapPost("/Register", async (User user,[FromServices]IMediator mediator) =>
{
    if (await mediator.Send(new GetByNameUserQuery(user.Name)) is not null)
        Results.NotFound("User Existing");
    var result = await mediator.Send(new CreateUserCommand(user));
    return result ? Results.Created() : Results.BadRequest();
});


app.UseHttpsRedirection();



app.Run();
