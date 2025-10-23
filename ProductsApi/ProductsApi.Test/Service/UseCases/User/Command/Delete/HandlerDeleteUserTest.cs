using ProductsApi.Application.UseCases.User.Command.Create;
using ProductsApi.Application.UseCases.User.Command.Delete;
using ProductsApi.Domain.BackOffice.ObjectValue;
using ProductsApi.Test.Mocks;

namespace ProductsApi.Test.Service.UseCases.User.Command.Delete;

public class HandlerDeleteUserTest
{
    private DeleteUserHandler _handler = new DeleteUserHandler(new FakeUniOfWork());

    [Fact]
    public async Task DeleteUserNull_Return_False()
    {
        //arrange
        DeleteUserCommand command = new DeleteUserCommand(null);
        //act
        var result = await _handler.HandleAsync(command);
        //assert
        Assert.Equal(result,false);
    }
    [Fact]
    public async Task DeleteUserOk_Return_True()
    {
        //arrange
        DeleteUserCommand command = new DeleteUserCommand(new
                ("kaiky", "teste", new Email("kaiky@gmail.com"))
        );
        //act
        var result = await _handler.HandleAsync(command);
        //assert
        Assert.Equal(result,true);
    }
}