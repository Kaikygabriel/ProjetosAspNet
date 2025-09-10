using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatalogoApi.Controllers;
using CatalogoApi.Model.Dto;
using CatalogoApi.Pagination;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CatalogoApiTests.UnitTest
{
    public class GetProdutosUnitTests : IClassFixture<ProdutosUnitTestController>
    {
        private readonly ProdutosController _controller;
        public GetProdutosUnitTests(ProdutosUnitTestController controller)
        {
            _controller = new ProdutosController(controller._unitOfWork,controller.mapper);
        }
        [Fact]
        public async Task GetProdutoById_Return_OkResult()
        {
            //Arrange
            var produtoId = 1;

            //act
            var data = await _controller.GetAsync(produtoId);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(data.Result);
            Assert.Equal(200, okResult.StatusCode);
        }
        [Fact]
        public async Task GetProdutoById_Return_NotFound()
        {
            //Arrange 
            var produtoId = 999;

             //act
            var data = await _controller.GetAsync(produtoId);

            //Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(data.Result);
            Assert.Equal(404, notFoundResult.StatusCode);
        }
        [Fact]
        public async Task GetProdutoById_Return_BadRequest()
        {
            //Arrange 
            int? produtoId = null;

            //act
            var data = await _controller.GetAsync(produtoId);

            //Assert
            var result = Assert.IsType<BadRequestResult>(data.Result);
            Assert.Equal(400, result.StatusCode);
        }
        [Fact]
        public async Task GetProdutos_Return_ListOfProdutoDTO()
        {

            //Act
            var data = await _controller.GetAsync();
            
            //assert
            var okResult = Assert.IsType<OkObjectResult>(data.Result); // Verifica se é OkObjectResult
            var produtos = Assert.IsAssignableFrom<IEnumerable<ProdutoDTO>>(okResult.Value); // Verifica o conteúdo
            Assert.NotNull(produtos);
        }
    }
}
