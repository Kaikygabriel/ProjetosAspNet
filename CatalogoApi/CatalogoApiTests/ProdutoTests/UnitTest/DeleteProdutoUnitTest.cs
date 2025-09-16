using System.Collections.Immutable;
using CatalogoApi.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace CatalogoApiTests.ProdutoTests.UnitTest;

public class DeleteProdutosUnitTests : IClassFixture<ProdutosUnitTestController>
{
    private readonly ProdutosController _controller;
    public DeleteProdutosUnitTests(ProdutosUnitTestController controller)
    {
        _controller = new ProdutosController(controller._unitOfWork, controller.mapper);
    }

    [Fact]
    public async Task DeleteProdutoById_Return_OkResult()
    {
        //Arrange 
        var idProduto = 1;
        
        //Act
        var data = await _controller.DeleteAsync(idProduto);
        
        //Assert
        var result = Assert.IsType<OkObjectResult>(data);
        Assert.Equal(200,result.StatusCode);
    }
    
    [Fact]
    public async Task DeleteProdutoById_Return_NotFound()
    {
        //Arrange 
        var idProduto = 1000;
        
        //Act
        var data = await _controller.DeleteAsync(idProduto);
        
        //Assert
        var result = Assert.IsType<NotFoundObjectResult>(data);
        Assert.Equal(404,result.StatusCode);
    }
}