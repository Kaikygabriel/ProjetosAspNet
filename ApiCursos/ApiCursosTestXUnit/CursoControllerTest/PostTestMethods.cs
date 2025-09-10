using ApiCursos.Controllers;
using APiCursos.Model;
using APiCursos.Model.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ApiCursosTestXUnit.CursoControllerTest;

public class PostTestMethods : IClassFixture<CursoUnitTest>
{
    private readonly CursosController controller;
    public PostTestMethods(CursoUnitTest unitTest)
    {
        controller = new CursosController(unitTest.repository);
    }

    [Fact]
    public async Task PostCurso_Return_CreatedResult()
    {
        //Arrange 
        CursoDTO curso = new CursoDTO
        {
            Titulo = "Curso Teste",
            Autor = "Teste",
            Id = 3
        };        
        //Act
        var data = await controller.PostAsync(curso);
        
        //Assert
        var result = Assert.IsType<CreatedAtRouteResult>(data.Result);
        Assert.Equal(201,result.StatusCode);
    }
    [Fact]
    public async Task PostCurso_Return_BadRequestResult()
    {
        //Arrange 
        CursoDTO curso = null;
        
        //Act
        var data = await controller.PostAsync(curso);
        
        //Assert
        var result = Assert.IsType<BadRequestObjectResult>(data.Result);
        Assert.Equal(400,result.StatusCode);
    }
}