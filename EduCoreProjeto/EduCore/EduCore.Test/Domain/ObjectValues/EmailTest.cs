using EduCore.Domain.Exceptions;
using EduCore.Domain.ValueObjects;

namespace EduCore.Test.Domain.ObjectValues;

public class EmailTest
{
    [Fact]
    public void CreateEmailWithAdressNull_Return_EmailException()
    {
        Assert.Throws<EmailException>(() =>
            new Email(null));
    }
    [Fact]
    public void CreateEmailWithAdressEmpty_Return_EmailException()
    {
        Assert.Throws<EmailException>(() =>
            new Email(""));
    }
    [Fact]
    public void CreateEmailWithAdressOk_Return_Ok()
    {
        //arrange
        var adress = "teste@gmail";
        //act
        var email = new Email(adress);
        //assert
        Assert.Equal(adress,email.Adress);
    }
}