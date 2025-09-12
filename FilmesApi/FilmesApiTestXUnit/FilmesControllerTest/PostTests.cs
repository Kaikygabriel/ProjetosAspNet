using FilmesApi.Controllers;
using FilmesApi.Models.DTO;
using FilmesApiTestXUnit.Mocks;
using Microsoft.AspNetCore.Mvc;

namespace FilmesApiTestXUnit.FilmesControllerTest;

public class PostTests
{
    private readonly FilmesController controller;

    public PostTests()
    {
        controller = new FilmesController(new FakeUnitOfWork());
    }

    [Fact]
    public void PostTest_Return_CreatedAtRoute()
    {
        //Arrange
        FilmesDTO filme = new()
        {
            Autor = "kaiky",
            Id = 29,
            Titulo = "Kaiky eos kaikys"
        };
        
        //act
        var data = controller.Post(filme);
        
        //Assert
        var result = Assert.IsType<CreatedAtRouteResult>(data.Result);
        Assert.Equal(201,result.StatusCode);
    }

    [Fact]
    public void PostTest_Return_BadRequest()
    {
        //Arrange
        FilmesDTO filme = null;
        //act
        var data = controller.Post(filme);
        //Assert
        var result = Assert.IsType<BadRequestResult>(data.Result);
        Assert.Equal(400, result.StatusCode);
    }
}