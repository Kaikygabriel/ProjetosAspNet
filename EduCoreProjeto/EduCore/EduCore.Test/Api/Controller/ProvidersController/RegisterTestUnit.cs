using EduCore.Api.Controllers;
using EduCore.Application.DTOS.Provider;
using EduCore.Application.Services;
using EduCore.Infraestruct.Repositorys;
using EduCore.Test.Mocks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace EduCore.Test.Api.Controller.ProvidersController;

public class RegisterTestUnit
{
    private readonly EduCore.Api.Controllers.ProvidersController _controller;

    public RegisterTestUnit()
    {
        _controller = InstanceProvidersController.CreateProviderController();
    }

    [Fact]
    public async Task RegisterProviderNull_Return_BadRequestResult()
    {
        //arrange
        RegisterProviderDto providerNull = null;
        //act
        var data = await _controller.Register(providerNull);
        //assert
        Assert.IsType<BadRequestResult>(data);
    }
    
    [Fact]
    public async Task RegisterProviderExisting_Return_NotFoundResult()
    {
        //arrange
        RegisterProviderDto providerExisting = new()
        {
            Name = "Kaiky",
            AdressEmail = "kaiky@example.com",
            Password = "senhaSegura2"
        };
        //act
        var data = await _controller.Register(providerExisting);
        //assert
        Assert.IsType<NotFoundResult>(data);
    }
    
    [Fact]
    public async Task RegisterProviderOk_Return_CreatedResult()
    {
        //arrange
        RegisterProviderDto provider = new()
        {
            Name = "teste",
            AdressEmail = "teste@example.com",
            Password = "kajdsflajsdf"
        };
        //act
        var data = await _controller.Register(provider);
        //assert
        Assert.IsType<CreatedResult>(data);
    }
}