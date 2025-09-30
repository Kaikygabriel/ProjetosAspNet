using Filmes.Api.Controllers;
using Filmes.Application.DTOS;
using Filmes.Application.Services;
using Filmes.Domain.ObjectValue;
using FilmesApiTestXUnit.Mocks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace FilmesApiTestXUnit.Api.Controllers.TestUserController;

public class PostRegisterTestunit
{
    private readonly AuthController controller;
    
    private IConfiguration configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .Build();
    
    public PostRegisterTestunit()
    {
        controller = new AuthController(new ServiceRepositoryUser(
            new FakeUnitOfWork(), new MemoryCache(new MemoryCacheOptions())),new TokenService(),configuration);
    }
    
    
    [Fact]
    public async Task PostRegister_Return_NoContentResult()
    {
        //arrange
        var user = new RegisterModel()
        {
            Password = "kalsdfjlaksjdf",
            Name = "kaiky",
            Email = "kaiky@gmail.com"
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
        RegisterModel user = null;
        //act
        var data = await controller.RegisterUserAsync(user);
        //assert
        Assert.IsType<NotFoundResult>(data);
    }
    [Fact]
    public async Task PostRegister_Return_BadRequest()
    {
        //arrange
        var user = new RegisterModel()
        {
            Password = "jjj",//senha menor que 6 caracters
            Name = "Bruno Oliveira" // nome ja existente
        };
        //act
        var data = await controller.RegisterUserAsync(user);
        //assert
        Assert.IsType<BadRequestResult>(data);
    }
}
