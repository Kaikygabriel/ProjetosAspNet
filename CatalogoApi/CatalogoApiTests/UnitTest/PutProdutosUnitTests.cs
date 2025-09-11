using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatalogoApi.Controllers;
using CatalogoApi.Model.Dto;
using Microsoft.AspNetCore.Mvc;

namespace CatalogoApiTests.UnitTest
{
    public class PutProdutosUnitTests : IClassFixture<ProdutosUnitTestController>
    {
        private readonly ProdutosController _controller;
        public PutProdutosUnitTests(ProdutosUnitTestController controller)
        {
            _controller = new ProdutosController(controller._unitOfWork, controller.mapper);
        }
        [Fact]
        public async Task PutProduto_Update_Return_OkResult()
        {
            int idProduto = 1;
            ProdutoDTO produtoDto = new()
            {
                Id = 1,
                Nome = "teste",
                CategoriaId = 1,
                Descricao = "Teste ",
                ImagemUrl = "Teste.Url",
                Preco = 111
            };
            
            //act
            var data = await _controller.PutAsync(idProduto, produtoDto);
            
            //Assert 
            var result = Assert.IsType<OkObjectResult>(data);
            Assert.Equal(200,result.StatusCode);
        }
        
        [Fact]
        public async Task PutProduto_Update_Return_BadRequest()
        {
            //Arrange 
            int idProduto = 1;
            ProdutoDTO produtoDto = new()
            {
                Id = 2
            };
            //act
            var data = await _controller.PutAsync(idProduto, produtoDto);
            
            //Assert 
            var result = Assert.IsType<BadRequestResult>(data);
            Assert.Equal(400,result.StatusCode);
        }
    }
}
