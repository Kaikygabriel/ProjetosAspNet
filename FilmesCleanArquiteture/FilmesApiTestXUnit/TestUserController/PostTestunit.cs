using Filmes.Api.Controllers;
using Filmes.Application.DTOS;
using Filmes.Application.Services;
using Filmes.Domain.Entities;
using FilmesApiTestXUnit.Mocks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace FilmesApiTestXUnit.TestUserController;

public class PostTestunit
{
    private readonly AuthController controller;

    public PostTestunit()
    {
        controller = new AuthController(new ServiceRepositoryUser(
            new FakeUnitOfWork(), new MemoryCache(new MemoryCacheOptions())));
    }

    [Fact]
    public async Task PostRegister_Return_NoContentResult()
    {
        //arrange
        var user = new LoginModel()
        {
            Password = "kalsdfjlaksjdf",
            Name = "kaiky"
        };
        //act
        var data = await controller.RegisterUserAsync(user);
        //assert
        Assert.IsType<NoContentResult>(data);
    }
    [Fact]
    public async Task PostRegister_Return_NotFoundResult()
    {
        //arrange
        LoginModel user = null;
        //act
        var data = await controller.RegisterUserAsync(user);
        //assert
        Assert.IsType<NotFoundResult>(data);
    }
    [Fact]
    public async Task PostRegister_Return_BadRequest()
    {
        //arrange
        var user = new LoginModel()
        {
            Password = "jjj",
            Name = "Bruno Oliveira"
        };
        //act
        var data = await controller.RegisterUserAsync(user);
        //assert
        Assert.IsType<BadRequestResult>(data);
    }
}
