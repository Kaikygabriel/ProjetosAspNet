using LojaApi.Application.UseCases.Product.Commands.Create;
using LojaApi.Application.UseCases.Product.Commands.Delete;
using LojaApi.Application.UseCases.Product.Commands.Update;
using LojaApi.Domain.BackOffice.Entitys;
using LojaApi.Domain.BackOffice.Interfaces;
using LojaApi.Infraestruct.DependencyInjection;
using MediatorX.Core.Abstraction.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDepencyInjection(builder.Configuration);
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapGet("/Products", async (IUnitOfWork unit)
    => await unit.RepositoryProduct.GetAllAsync());

app.MapPost("/Products", async (Product product, IMediator mediator) =>
{
    if (product is null)
        return Results.BadRequest();
    var response = await mediator.SendAsync(new CreateProductRequest(product));
    return response ? Results.Created() : Results.NotFound();
});
app.MapPut("/Products/{id:int:min(1)}", async (int id,Product product, IMediator mediator) =>
{
    if (product is null || id != product.Id)
        return Results.BadRequest();
    
    var response = await mediator.SendAsync(new UpdateProductRequest(product));
    return response ? Results.Ok(product) : Results.NotFound();
});
app.MapDelete("/Products", async (Product product, IMediator mediator) =>
{
    if (product is null)
        return Results.BadRequest();
    var response = await mediator.SendAsync(new DeleteProductRequest(product));
    return response ? Results.Ok(product) : Results.NotFound();
});

app.UseHttpsRedirection();
app.Run();
