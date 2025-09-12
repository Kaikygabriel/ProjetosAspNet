using ApiCursos.Controllers;
using APiCursos.Model.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ApiCursosTestXUnit.CursoControllerTest;

public class GetTestMethods  : IClassFixture<CursoUnitTest>
{
    private readonly CursosController controller;

    public GetTestMethods(CursoUnitTest cursoUnitTest)
    {
        controller = new CursosController(cursoUnitTest.repository);
    }

    [Fact]
    public async Task GetCursobyId_Return_OkResult()
    {
        //Arrange
        var id = 1;
        
        //Act
        var data = await controller.GetAsync(id);
        
        //Assert
        var result = Assert.IsType<OkObjectResult>(data.Result);
        Assert.Equal(200,result.StatusCode);
    }

    [Fact]
    public async Task GetCursoById_Return_NotFoundResult()
    {
        //Arrange
        var id = 0;
        
        //Act
        var data = await controller.GetAsync(id);
        
        //Assert
        var result = Assert.IsType<NotFoundObjectResult>(data.Result);
        Assert.Equal(404,result.StatusCode);
    }
    
    [Fact]
    public async Task GetCurso_Return_ListOfCursoDtoAndResultOk()
    {
        //Act
        var data = await controller.GetAsync();
        
        //Assert 
        var result = Assert.IsType<OkObjectResult>(data.Result);
        Assert.IsAssignableFrom<IEnumerable<CursoDTO>>(result.Value);
        Assert.Equal(200,result.StatusCode);
    } 
}