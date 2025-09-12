using FilmesApi.Controllers;
using FilmesApi.Models.DTO;
using FilmesApiTestXUnit.Mocks;
using Microsoft.AspNetCore.Mvc;

namespace FilmesApiTestXUnit.FilmesControllerTest;

public class GetTests
{
    private readonly FilmesController _controller;

    public GetTests()
    {
        _controller = new FilmesController(new FakeUnitOfWork());
    }

    [Fact]
    public void GetFilmes_Return_OkObjectResult()
    {
        //act
        var data = _controller.Get();

        //assert
        var result = Assert.IsType<OkObjectResult>(data.Result);
        Assert.Equal(200, result.StatusCode);
        Assert.IsAssignableFrom<IEnumerable<FilmesDTO>>(result.Value);
    }

    [Fact]
    public void GetFilmes_Return_NotFoundObjectResult()
    {
        //Arrange
        var id = 100;

        //act
        var data = _controller.Get(id);

        //assert
        var result = Assert.IsType<NotFoundObjectResult>(data.Result);
        Assert.Equal(404, result.StatusCode);
    }
}