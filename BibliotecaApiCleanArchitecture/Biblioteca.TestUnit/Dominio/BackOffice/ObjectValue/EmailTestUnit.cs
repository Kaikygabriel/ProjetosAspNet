using Biblioteca.Domain.BackOffice.Exceptions;
using Biblioteca.Domain.BackOffice.ObjectValues;

namespace Biblioteca.TestUnit.Dominio.BackOffice.ObjectValue;

public class EmailTestUnit
{
    [Fact]
    public void CreateEmailNull_Return_EmailException()
    {
        Assert.Throws<EmailException>(() =>
            new Email(null));
    }
    [Fact]
    public void CreateEmailEmpty_Return_EmailException()
    {
        Assert.Throws<EmailException>(() =>
            new Email(string.Empty));
    } 
    [Fact]
    public void CreateEmail_Return_Ok()
    {
        //arrange
        var  adress = "teste@gmail.com";
        //act
        var email = new Email(adress);
        //assert
        Assert.Equal(email.Adress,adress);
    } 
}