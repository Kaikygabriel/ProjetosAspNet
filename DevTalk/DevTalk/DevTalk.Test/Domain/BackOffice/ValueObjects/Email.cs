using DevTalk.Domain.BackOffice.Exception.Email;

namespace DevTalk.Test.Domain.BackOffice.ValueObjects;

public class Email
{
    private const string EmailValid = "Teste@gmail.com";
    private const string EmailInvalid = "teste";
    private const string? EmailNull = null;
    private const string? EmailEmpty = "";

    [Fact]
    public void Create_Email_WithAddressInvalid_Return_EmailException()
    {
        Assert.Throws<EmailException>(() =>
        {
            var email = new DevTalk.Domain.BackOffice.ObjectValue.Email(EmailInvalid);
        });
    }
    [Fact]
    public void Create_Email_WithAddressNull_Return_EmailException()
    {
        Assert.Throws<EmailException>(() =>
        {
            var email = new DevTalk.Domain.BackOffice.ObjectValue.Email(EmailNull);
        });
    }
    [Fact]
    public void Create_Email_WithAddressEmpty_Return_EmailException()
    {
        Assert.Throws<EmailException>(() =>
        {
            var email = new DevTalk.Domain.BackOffice.ObjectValue.Email(EmailEmpty);
        });
    }
    
    [Fact]
    public void Create_Email_WithAddressValid_Return_EmailException()
    { 
        var email = new DevTalk.Domain.BackOffice.ObjectValue.Email(EmailValid);
        Assert.Equal(email.Address,EmailValid);
    }
}