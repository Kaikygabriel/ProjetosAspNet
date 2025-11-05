using DevTalk.Domain.BackOffice.Entities;
using DevTalk.Domain.BackOffice.Exception.User;

namespace DevTalk.Test.Domain.BackOffice.Entities;

public class UserTest
{
    private const string Email = "teste@gmail.com";
    
    private const string? NameNull = null;
    private const string NameEmpty = "";
    private const string NameValid = "teste";

    private const string PasswordEmpty = "";
    private const string PasswordInvalid = "123";
    private const string PasswordValid = "Aldlafsjdlafj";

    [Fact]
    public void Create_User_WithPasswordInvalidAndNameEmpty_Return_UserException()
    {
        Assert.Throws<UserException>(() =>
        {
            var user = new User(NameEmpty, PasswordInvalid, Email);
        });
    }
    [Fact]
    public void Create_User_WithPasswordEmptyAndNameNull_Return_UserException()
    {
        Assert.Throws<UserException>(() =>
        {
            var user = new User(NameNull,PasswordEmpty, Email);
        });
    }
    [Fact]
    public void Create_User_WithPasswordOkAndNameOk_Return_UserException()
    { 
        
        //arrange                    //act
        var user = new User(NameValid,PasswordValid, Email);
        //assert
        Assert.Equal(user.Name,NameValid);
        Assert.Equal(user.Password,PasswordValid);
    }
}