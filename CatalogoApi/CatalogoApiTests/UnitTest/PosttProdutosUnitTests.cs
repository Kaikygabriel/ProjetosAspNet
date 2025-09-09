using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using CatalogoApi.Controllers;
using CatalogoApi.Model;
using CatalogoApi.Model.Dto;
using Microsoft.AspNetCore.Mvc;

namespace CatalogoApiTests.UnitTest
{
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
         
        }
        [Fact]
        public async Task PostProduto_Return_BadRequest()
        {

        }
    }
}
