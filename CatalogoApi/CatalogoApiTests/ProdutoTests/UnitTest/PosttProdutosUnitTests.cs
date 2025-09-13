using CatalogoApi.Controllers;
using CatalogoApi.Model.Dto;
using Microsoft.AspNetCore.Mvc;

namespace CatalogoApiTests.ProdutoTests.UnitTest;

    public class PostProdutosUnitTests  :IClassFixture<ProdutosUnitTestController>
    {
        private readonly ProdutosController _controller;
        public PostProdutosUnitTests(ProdutosUnitTestController controller)
        {
            _controller = new ProdutosController(controller._unitOfWork, controller.mapper);
        }
        [Fact]
        public async Task PostProduto_Return_CreatedStatusCode()
        {
            //Arrange
            ProdutoDTO produto = new()
            {
                Preco= 111,
                Nome = "Teste",
                Descricao = "Fazendo teste",
                ImagemUrl = "teste.url",
                CategoriaId = 1
            };
            
            //act
            var data = await _controller.PostAsync(produto);
            
            //Assert
            var result = Assert.IsType<CreatedAtRouteResult>(data);
            Assert.Equal(201,result.StatusCode);
        }
        [Fact]
        public async Task PostProduto_Return_BadRequest()
        {
            //Arrange
            ProdutoDTO? produtoNull = null;
            
            //Act
            var data = await _controller.PostAsync(produtoNull);
            
            //Assert
            var result = Assert.IsType<BadRequestObjectResult>(data);
            Assert.Equal(400,result.StatusCode);
        }
    }

