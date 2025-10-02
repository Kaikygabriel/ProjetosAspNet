using Biblioteca.Application.DTOS;
using Biblioteca.Application.Services;
using Biblioteca.TestUnit.Mocks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Biblioteca.TestUnit.Api.Controllers.AuthController;

public class LoginTest
{
    private readonly IConfiguration config;
    private readonly Biblioteca.Api.Controllers.AuthController controller;

    public LoginTest()
    {
        config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
        controller = new Biblioteca.Api.Controllers.AuthController(new FakeUnitOfWork(), new TokenService(), config);
    }

    [Fact]
    public async Task LoginUserNull_Return_BadRequestResult()
    {
        //arrange
        LoginUserDTO user = null;
        //act
        var data = await controller.LoginUserAsync(user);
        //assert
        var result = Assert.IsType<BadRequestResult>(data);
        Assert.Equal(400, result.StatusCode);
    }

    [Fact]
    public async Task LoginUserNoExist_Return_NotFoundResult()
    {
        //arrange
        LoginUserDTO user = new LoginUserDTO("UserNOExist", "senhaTeste");
        //act
        var data = await controller.LoginUserAsync(user);
        //assert
        var result = Assert.IsType<NotFoundResult>(data);
        Assert.Equal(404, result.StatusCode);
    }

    [Fact]
    public async Task LoginUserPasswordInvalid_Return_NotFoundResult()
    {
        //arrange
        LoginUserDTO user = new LoginUserDTO("Lucas", "senhaErrada");
        //act
        var data = await controller.LoginUserAsync(user);
        //assert
        var result = Assert.IsType<NotFoundResult>(data);
        Assert.Equal(404, result.StatusCode);
    }

    [Fact]
    public async Task LoginUservalid_Return_OkObjectResult()
    {
        //arrange
        LoginUserDTO user = new LoginUserDTO("Lucas", "lucas789");
        //act
        var data = await controller.LoginUserAsync(user);
        //assert
        var result = Assert.IsType<OkObjectResult>(data);
        Assert.Equal(200, result.StatusCode);
    }

    [Fact]
    public async Task LoginUserPasswordNull_Return_BadRequestResult()
    {
        //arrange
        LoginUserDTO user = new LoginUserDTO("Lucas", null);
        //act
        var data = await controller.LoginUserAsync(user);
        //assert
        var result = Assert.IsType<BadRequestResult>(data);
        Assert.Equal(400, result.StatusCode);
    }

}