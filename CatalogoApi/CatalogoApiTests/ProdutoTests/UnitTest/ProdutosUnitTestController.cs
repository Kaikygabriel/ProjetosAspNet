using AutoMapper;
using CatalogoApi.Repository.Interface;
using CatalogoApiTests.Mocks;

namespace CatalogoApiTests.ProdutoTests.UnitTest;

    public class ProdutosUnitTestController
    {
        public IUnitOfWork _unitOfWork;
        public IMapper mapper;
       
        public ProdutosUnitTestController()
        {
            _unitOfWork = new FakeUnitOfWork();
        }
    }

