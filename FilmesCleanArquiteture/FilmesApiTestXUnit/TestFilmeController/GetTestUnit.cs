using Filmes.Api.Controllers;
using Filmes.Domain.Entities;
using Filmes.Infraestruture.Repository;
using FilmesApiTestXUnit.Mocks;
using Microsoft.AspNetCore.Mvc;

namespace FilmesApiTestXUnit.TestFilmeController;

public class GetTestUnit
{
    private readonly FilmesController _controller;

    public GetTestUnit()
    {
        _controller = new FilmesController(new FakeUnitOfWork());
    }

    [Fact]
    public async Task GetFilmes_Return_OkObjectResult()
    {
        //act
        var data = await _controller.GetAsync();
        //assert
        var result = Assert.IsType<OkObjectResult>(data);
        Assert.IsAssignableFrom<List<Filme>>(result.Value);
        Assert.Equal(200, result.StatusCode);
    }
}