using ProductsApi.Application.UseCases.Product.Query.GetAll;
using ProductsApi.Test.Mocks;

namespace ProductsApi.Test.Service.UseCases.Product.Query.GetAll;

public class GetAllProductHandlerTest
{
    private  GetAllProductHandler _handler = new GetAllProductHandler(new FakeUniOfWork());

    [Fact]
    public async Task GetAllProductsHandlerOk_Return_IEnumerableFromProducts()
    {
        var data = new GetAllProductsQuery();
        var result = await _handler.HandleAsync(data,
            TestContext.Current.CancellationToken);
        Assert.IsType<IEnumerable<Domain.BackOffice.Entitys.Product>>(result, exactMatch: false);
    }
}