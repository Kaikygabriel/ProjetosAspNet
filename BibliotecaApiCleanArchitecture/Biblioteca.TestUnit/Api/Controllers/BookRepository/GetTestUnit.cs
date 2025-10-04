using Biblioteca.Api.Controllers;
using Biblioteca.TestUnit.Mocks;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.TestUnit.Api.Controllers.BookRepository;

public class GetTestUnit
{
    private readonly BooksController controller;

    public GetTestUnit()
    {
        controller = new BooksController(new FakeUnitOfWork());
    }

    [Fact]
    public async Task GetUserById_Return_OkObjectResult()
    {
        var id = 1;

        var data = await controller.GetByidAsync(id);

        Assert.IsType<OkObjectResult>(data);
    }
    [Fact]
    public async Task GetUserNullByIdReturn_NotFoundResult()
    {
        var id = 999;

        var data = await controller.GetByidAsync(id);

        Assert.IsType<NotFoundResult>(data);
    }
}