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
        _controller = new FilmesController(new FakeFilmeServiceRepository());
    }

    [Fact]
    public async Task GetFilmes_Return_OkObjectResult()
    {
        //act
        var data = await _controller.GetAsync();
        //assert
        var result = Assert.IsType<OkObjectResult>(data);
        Assert.Equal(200, result.StatusCode);
    }
    
    [Fact]
    public async Task GetFilmeById_Return_OkObjectResult()
    {
        //arrange
        var id = 1 ;
        //act
        var data = await _controller.GetAsync(id);
        //assert
        var result = Assert.IsType<OkObjectResult>(data);
        Assert.Equal(200, result.StatusCode);
    }
    [Fact] 
    public async Task GetFilmeById_Return_BadRequestResult()
    {
        //arrange
        var id = 999;
        //Act
        var data = await   _controller.GetAsync(id);
        //assert
        var result = Assert.IsType<BadRequestResult>(data);
        Assert.Equal(400,result.StatusCode);
    }
}
