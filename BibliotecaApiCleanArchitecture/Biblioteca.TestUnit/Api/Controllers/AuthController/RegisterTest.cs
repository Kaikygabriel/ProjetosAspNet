using Biblioteca.Application.DTOS;
using Biblioteca.Application.Services;
using Biblioteca.Infraestructure.Repositorys;
using Biblioteca.TestUnit.Mocks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Biblioteca.TestUnit.Api.Controllers.AuthController;

public class RegisterTest
{
    
    private readonly IConfiguration config;
    private readonly Biblioteca.Api.Controllers.AuthController controller;

    public RegisterTest()
    {
       config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
        controller= new Biblioteca.Api.Controllers.AuthController(new FakeUnitOfWork(),new TokenService(),config);
    }

    [Fact]
    public async Task RegisterUserNull_Return_BadRequestResult()
    {
        var data = await controller.RegisterUserAsync(null);
        
        var result = Assert.IsType<BadRequestResult>(data);
        Assert.Equal(400,result.StatusCode);
    }
    [Fact]
    public async Task RegisterUserExist_Return_BadRequestResult()
    {
        var userExist = new RegisterUserDTO()
        {
            Name = "Carlos"
        };
        
        var data = await controller.RegisterUserAsync( userExist);
        
        var result = Assert.IsType<NotFoundResult>(data);
        Assert.Equal(404,result.StatusCode);
    }

    [Fact]
    public async Task RegisterUserOk_Return_CreatedResult()
    {
        var userExist = new RegisterUserDTO()
        {
            Name = "teste",
            EmailAdress = "teste@gmail.com",
            Password = "ajkfdslkafjslkdjf"
        };
        
        var data = await controller.RegisterUserAsync( userExist);
        
        var result = Assert.IsType<CreatedResult>(data);
        Assert.Equal(201,result.StatusCode);
    }
}