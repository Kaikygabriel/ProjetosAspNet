using Catalogo.Domain.Exceptions;
using Catalogo.Domain.ObjectValue;

namespace CatalogoXunitTest.Dominio.ObjectValues;

public class EmailTestUnit
{
    [Fact]
    public void CreateEmailWithAdressNull_Return_EmailException()
    {
        Assert.Throws<EmailException>(() 
            => new Email(null) 
        );
    }
    [Fact]
    public void CreateEmailWithAdressEmpty_Return_EmailException()
    {
        Assert.Throws<EmailException>(() 
            => new Email("") 
        );
    }
    [Fact]
    public void CreateEmailOk_Return_EmailWithAdressOk()
    {
        var adress = "teste@gmail.com";
        var email = new Email(adress);
        Assert.Equal(adress,email.Adress);
    }
}