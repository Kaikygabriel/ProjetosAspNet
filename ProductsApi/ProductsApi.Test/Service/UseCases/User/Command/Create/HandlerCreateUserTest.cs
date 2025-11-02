using Microsoft.AspNetCore.Builder;
using ProductsApi.Application.UseCases.Product.Command.Create;
using ProductsApi.Application.UseCases.User.Command.Create;
using ProductsApi.Domain.BackOffice.Exceptions;
using ProductsApi.Domain.BackOffice.ObjectValue;
using ProductsApi.Test.Mocks;

namespace ProductsApi.Test.Service.UseCases.User.Command.Create;

public class HandlerCreateUserTest
    
{
    private CreateUserHandler _handler = new CreateUserHandler(new FakeUniOfWork());
    
    [Fact]
    public async Task HandlerUserCreateWithCommandEmpty_Return_UserException()
    {
        await Assert.ThrowsAsync<UserException>(async () =>
            await _handler.HandleAsync( new CreateUserCommand
                (new ProductsApi.Domain.BackOffice.Entities.User("","",null))
                , TestContext.Current.CancellationToken));
    }
    [Fact]
    public async Task HandlerUserCreateWithNameEmpty_Return_UserException()
    {
        await Assert.ThrowsAsync<UserException>(async () =>
            await _handler.HandleAsync( new CreateUserCommand
                (new ProductsApi.Domain.BackOffice.Entities.User
                    ("","teste",new Email("Teste@gmail.com")))
                , TestContext.Current.CancellationToken));
    }
    [Fact]
    public async Task HandlerUserCreateWithPasswordEmpty_Return_UserException()
    {
        await Assert.ThrowsAsync<UserException>(async () =>
            await _handler.HandleAsync( new CreateUserCommand
                (new ProductsApi.Domain.BackOffice.Entities.User
                    ("teste","",new Email("Teste@gmail.com")))
                , TestContext.Current.CancellationToken));
    }
 
}