using DevTalk.Application.UseCases.User.Command.Create;
using DevTalk.Test.Mock;

namespace DevTalk.Test.Application.UseCases.User.Command.Create;

public class CreateTest
{
    private CreateUserHandler Handler = new(new FakeUnitOfWork());

    [Fact]
    public async Task Create_User_UserNull_Return_False()
    {
        //Arrange
        CreateUserCommand command = new CreateUserCommand(null);
        // act
        var result = await Handler.Handle(command,new CancellationToken());
        //assert
        Assert.Equal(false,result);
    }
    
    [Fact]
    public async Task Create_User_UserOk_Return_True()
    {
        //Arrange
        CreateUserCommand command = new CreateUserCommand
            (new DevTalk.Domain.BackOffice.Entities.User("Teste","aluno2022","teste@gmail"));
        // act
        var result = await Handler.Handle(command,CancellationToken.None);
        //assert
        Assert.Equal(true,result);
    }
}