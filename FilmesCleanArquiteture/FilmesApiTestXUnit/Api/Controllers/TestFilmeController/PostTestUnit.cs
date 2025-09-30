using Filmes.Api.Controllers;
using Filmes.Application.DTOS;
using Filmes.Application.Services;
using FilmesApiTestXUnit.Mocks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace FilmesApiTestXUnit.Api.Controllers.TestFilmeController;

public class PostTestUnit
{
    private readonly FilmesController controller;

    public PostTestUnit()
    {
        controller = new FilmesController(new FilmeServiceRepository(new FakeUnitOfWork(),
            new MemoryCache(new MemoryCacheOptions())));
    }

    [Fact]
    public async Task CreateFilme_Return_CreatedResult()
    {
        //arrange
        CreateFilmeDTO filme = new()
        {
            Titulo = "teste",
            Autor = "autorTeste",
            Categoria = "teste"
        };
        //act
        var data = await controller.CreateAsync(filme);
        //assert
        var result = Assert.IsType<CreatedResult>(data);
        Assert.Equal(201,result.StatusCode);
    } 
    [Fact]
    public async Task CreateFilmeNull_Return_BadRequestResult()
    {
        //arrange
        CreateFilmeDTO filme = null;
        //act
        var data = await controller.CreateAsync(filme);
        //assert
        var result = Assert.IsType<BadRequestResult>(data);
        Assert.Equal(400,result.StatusCode);
    }

    [Fact]
    public async Task CreateFilmeExist_Return_NotFoundResult()
    {
        //arrange
        var filme = new CreateFilmeDTO()
        {
            Titulo = "Interestelar"
        };
        //act
        var data = await controller.CreateAsync(filme);
        //assert
        Assert.IsType<NotFoundResult>(data);
    }
}