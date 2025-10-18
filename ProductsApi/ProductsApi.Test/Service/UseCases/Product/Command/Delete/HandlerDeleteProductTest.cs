using ProductsApi.Application.UseCases.Product.Command.Create;
using ProductsApi.Application.UseCases.Product.Command.Delete;
using ProductsApi.Domain.BackOffice.ObjectValue;
using ProductsApi.Test.Mocks;

namespace ProductsApi.Test.Service.UseCases.Product.Command.Delete;

public class HandlerDeleteProductTest
{
    private readonly DeleteProductHandler _handler = new(new FakeUniOfWork());
    

    [Fact]
    public async Task HandlerProduct_DeleteCommandWithProductNull_Return_False()
    {
        //arrange
        var deleteCommand = new DeleteProductCommand(null);
        //act
        var result = await _handler.HandleAsync(deleteCommand, TestContext.Current.CancellationToken);
        //assert
        Assert.Equal(result,false);
    }
    
    [Fact]
    public async Task HandlerProduct_DeleteCommandOk_Return_True()
    {
        //arrange
        var command = new DeleteProductCommand
            (new Domain.BackOffice.Entitys.Product(50.0m, new Category("Eletronico"),"mouse"));
        //act
        var result = await _handler.HandleAsync(command, TestContext.Current.CancellationToken);
        //assert
        Assert.Equal(result,true);
    }
}