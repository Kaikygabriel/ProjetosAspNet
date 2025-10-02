using Biblioteca.Domain.BackOffice.Entities;
using Biblioteca.Domain.BackOffice.Exceptions;
using Biblioteca.Domain.ObjectValues;

namespace Biblioteca.TestUnit.Dominio.BackOffice.Entities;

public class UserTestUnit
{
    [Fact]
    public void CreateUserNull_Return_UserException()
    {
        Assert.Throws<UserException>(()
            => new User(null, null, null));
    }
    
    [Fact]
    public void CreateUserEmpty_Return_UserException()
    {
        var stringEmpty = "";
        Assert.Throws<UserException>(()
            => new User(stringEmpty, stringEmpty, new Email("teste")));
    }
}