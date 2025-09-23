using Catalogo.API.Controllers;
using CatalogoXunitTest.Mocks;
using Microsoft.AspNetCore.Mvc;

namespace CatalogoXunitTest.ProdutosControllerTest;

public class GetTestsUnit
{
   private readonly ProdutosController controller;

   public GetTestsUnit()
   {
      controller = new ProdutosController(new FakeUnitOfWork());
   }

   [Fact]
   public async Task ProdutosById_Return_OkObjectResult()
   {
      //arrange
      var id = 1;
      //act
      var data = await controller.GetAsync(id);
      //assert
      var result =Assert.IsType<OkObjectResult>(data);
      Assert.Equal(200,result.StatusCode);
   }
   [Fact]
   public async Task ProdutosById_Return_NotFoundResult()
   {
      //arrange
      var id = 999;
      //act
      var data = await controller.GetAsync(id);
      //assert
      var result =Assert.IsType<NotFoundResult>(data);
      Assert.Equal(404,result.StatusCode);
   }
}