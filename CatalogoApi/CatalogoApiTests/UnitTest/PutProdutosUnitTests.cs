using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatalogoApi.Controllers;

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

        }
        
        [Fact]
        public async Task PutProduto_Update_Return_BadRequest()
        {

        }
    }
}
