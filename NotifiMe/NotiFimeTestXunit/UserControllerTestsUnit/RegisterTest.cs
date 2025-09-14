using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NotifiMe.Controllers;
using NotifiMe.Models.LoginModel;
using NotifiMe.Service;
using NotiFimeTestXunit.Mocks;

namespace NotiFimeTestXunit.UserControllerTestsUnit;

public class RegisterTest
{
    private readonly IConfiguration configuration;
    private readonly AuthUserController userController;

    public RegisterTest()
    {
        userController = new AuthUserController(new TokenService(), new FakeUniOfWork(), configuration);
    }

    [Fact]
    public async Task PostRegister_Return_NoContentResult()
    {
        //Assert
        var user = new LoginUserModel()
        {
            Name = "teste",
            Email = "teste@example.com",
            Password = Guid.NewGuid().ToString("N").Substring(0,12) // senha randômica
        };
        //Act
        var data = await userController.Register(user);
        
        //assert
        var result = Assert.IsType<NoContentResult>(data);
        Assert.Equal(204,result.StatusCode);
    }
    
    [Fact]
    public async Task PostRegister_Return_NotFoundResult()
    {
        //Assert
        var user = new LoginUserModel()
        {
            Name = "João Silva",//Ja existe um usuario com mesmo nome
            Email = "joao.silva@example.com",
            Password = Guid.NewGuid().ToString("N").Substring(0,12) 
        };
        
        //Act
        var data = await userController.Register(user);
        
        //assert
        var result = Assert.IsType<NotFoundResult>(data);
        Assert.Equal(404,result.StatusCode);
    }
    
    [Fact]
    public async Task PostRegister_Return_BadRequestObjectResult()
    {
        //Assert
        var user = new LoginUserModel()
        {
            Name = "teste",
            Email = "teste@example.com",
            Password = "a"//Senha menor que 6 caracters
        };
        //Act
        var data = await userController.Register(user);
        
        //assert
        var result = Assert.IsType<BadRequestObjectResult>(data);
        Assert.Equal(400,result.StatusCode);
    }
}