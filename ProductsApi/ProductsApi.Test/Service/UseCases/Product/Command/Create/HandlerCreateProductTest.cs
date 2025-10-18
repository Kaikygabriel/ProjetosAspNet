using ProductsApi.Application.UseCases.Product.Command.Create;
using ProductsApi.Test.Mocks;

namespace ProductsApi.Test.Service.UseCases.Product.Command.Create;

public class HandlerCreateProductTest
{
    private readonly CreateProductHandler _handler;

    public HandlerCreateProductTest()
    {
        _handler = new CreateProductHandler(new FakeUniOfWork());
    }

    public static IEnumerable<object[]> ProductData =>
        new List<object[]>
        {
            new object[] { new CreateProductCommand(null,null,default), false },
            new object[] { new CreateProductCommand(null,"categoria",50.0m), false },
            new object[] { new CreateProductCommand("Teclado", null, 50.0m),false},
            new object[] { new CreateProductCommand("Mouse","Periférico",150.0m),true}
        };

    [Theory]
    [MemberData(nameof(ProductData))]
    public async Task HandlerProduct_Create_Results(CreateProductCommand command,bool result)
    {
        var data = await _handler.HandleAsync(command, TestContext.Current.CancellationToken);
        Assert.Equal(data,result);
    }
}