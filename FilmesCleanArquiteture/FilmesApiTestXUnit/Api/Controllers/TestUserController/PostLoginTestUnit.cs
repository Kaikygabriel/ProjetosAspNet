using Filmes.Api.Controllers;
using Filmes.Application.DTOS;
using Filmes.Application.Services;
using FilmesApiTestXUnit.Mocks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace FilmesApiTestXUnit.Api.Controllers.TestUserController;

public class PostLoginTestUnit
{
    private readonly AuthController controller;
    
    private IConfiguration configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .Build();

    public PostLoginTestUnit()
    {
        controller = new AuthController(new ServiceRepositoryUser(
            new FakeUnitOfWork(), new MemoryCache(new MemoryCacheOptions())),new TokenService(),configuration);
    }

    [Fact]
    public async Task PostLogin_Return_OkObjectResult()
    {
        //arrange
        var loginModel = new LoginModel()
        {
            Name = "Daniel",
            Password = "$PLACEHOLDER_HASH_4",
        };
        //act
        var data = await controller.LoginUserAsync(loginModel);
        //assert
        var result = Assert.IsType<OkObjectResult>(data);
        Assert.Equal(200,result.StatusCode);
    }
    [Fact]
    public async Task PostLogin_Return_NotFoundResult()
    {
        //arrange
        var loginModel = new LoginModel()
        {
            Name = "UsuarioNãoExistente",
            Password = "teste",
        };
        //act
        var data = await controller.LoginUserAsync(loginModel);
        //assert
        var result = Assert.IsType<NotFoundResult>(data);
        Assert.Equal(404,result.StatusCode);
    }
    [Fact]
    public async Task PostLogin_Return_BadRequestResult()
    {
        //arrange
        LoginModel login = null;
        //act
        var data = await controller.LoginUserAsync(login);
        //assert
        var result = Assert.IsType<BadRequestResult>(data);
        Assert.Equal(400,result.StatusCode);
    }
}