using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NotifiMe.Controllers;
using NotifiMe.Models.LoginModel;
using NotifiMe.Service;
using NotiFimeTestXunit.Mocks;

namespace NotiFimeTestXunit.UserControllerTestsUnit;

public class LoginTests
{
    private readonly IConfiguration configuration;
    private readonly AuthUserController controller;

    public LoginTests()
    {
        var inMemorySettings = new Dictionary<string, string>
        {
            {"Jwt:SecretKey", "r4P07K26Auu1wuKmRaGNufNfFEDbNsQRb0Dwy"},
            {"Jwt:Issuer", "http://localhost:5107"},
            {"Jwt:Audience", "http://localhost:7296"}
        };

        configuration = new ConfigurationBuilder().AddInMemoryCollection(inMemorySettings!).Build();
       
        controller = new AuthUserController(new TokenService(), new FakeUniOfWork(),
            configuration);
    }
    
    [Fact]
    public async Task PostLogin_Return_OkObjectResult()
    {
        //arrange
        LoginUserModel modelLogin = new()
        {
            Name = "João Silva",
            Password = "fjas9832r3h",
            Email ="joao.silva@example.com"
        };
        //Act
        var data = await controller.Login(modelLogin);
        //assert
        var result = Assert.IsType<OkObjectResult>(data);
        Assert.Equal(200,result.StatusCode);
    }

    [Fact]
    public async Task PostLoginflawed_Return_Unauthorized()
    {
        //Arrange -- user not authorized
        LoginUserModel modelLogin = new()
        {
            Name = "testNoAuthorized",
            Password = "test"
        };
        //Act
        var data = await controller.Login(modelLogin);
        //assert
        var result = Assert.IsType<UnauthorizedResult>(data);
        Assert.Equal(401,result.StatusCode);
    }
}
