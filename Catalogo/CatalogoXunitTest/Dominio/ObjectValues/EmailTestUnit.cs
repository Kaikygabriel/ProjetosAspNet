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
}