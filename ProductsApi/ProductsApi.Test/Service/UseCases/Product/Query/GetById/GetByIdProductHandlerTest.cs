using ProductsApi.Application.UseCases.Product.Query.GetById;
using ProductsApi.Test.Mocks;

namespace ProductsApi.Test.Service.UseCases.Product.Query.GetById;

public class GetByIdProductHandlerTest
{
    private GetByIdProductHandler _handlerTest = new GetByIdProductHandler(new FakeUniOfWork());

    [Fact]
    public async Task GetByProductIdNull_Return_Null()
    {
        var result = await _handlerTest.HandleAsync(null);
        Assert.Null(result);
    }
    [Fact]
    public async Task GetByProductIdInvalid_Return_Null()
    {
        var result = await _handlerTest.HandleAsync(new GetByIdQuery(1000),
            TestContext.Current.CancellationToken);
        Assert.Null(result);
    }
    [Fact]
    public async Task GetByProductIdOk_Return_Product()
    {
        var result = await _handlerTest.HandleAsync(new GetByIdQuery(1),
            TestContext.Current.CancellationToken);
        Assert.IsType<ProductsApi.Domain.BackOffice.Entities.Product>(result);
    }
}