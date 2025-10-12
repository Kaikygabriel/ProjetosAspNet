using EduCore.Application.DTOS.Provider;
using EduCore.Application.DTOS.Student;
using Microsoft.AspNetCore.Mvc;

namespace EduCore.Test.Api.Controller.StudentsController;

public class RegisterTestUnit
{
    private readonly EduCore.Api.Controllers.StudentsController _controller;

    public RegisterTestUnit()
    {
        _controller = InstanceStudentController.CreateProviderController();
    }
    
    [Fact]
    public async Task RegisterStudentNull_Return_BadRequestResult()
    {
        //arrange
        RegisterStudentDTO studentNull = null;
        //act
        var data = await _controller.RegisterAsync(studentNull);
        //assert
        Assert.IsType<BadRequestResult>(data);
    }
    
    [Fact]
    public async Task RegisterStudentExisting_Return_NotFoundResult()
    {
        //arrange
        RegisterStudentDTO studentExisting = new()
        {
            Name = "Kaiky",
            AdressEmail = "kaiky@example.com",
            Password = "senhaSegura2"
        };
        //act
        var data = await _controller.RegisterAsync(studentExisting);
        //assert
        Assert.IsType<NotFoundResult>(data);
    }
    
    [Fact]
    public async Task RegisterStudentOk_Return_CreatedResult()
    {
        //arrange
        RegisterStudentDTO student = new()
        {
            Name = "teste",
            AdressEmail = "teste@example.com",
            Password = "kajdsflajsdf"
        };
        //act
        var data = await _controller.RegisterAsync(student);
        //assert
        Assert.IsType<CreatedResult>(data);
    }
}