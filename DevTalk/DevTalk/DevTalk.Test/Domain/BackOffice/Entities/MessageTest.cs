using DevTalk.Domain.BackOffice.Entities;
using DevTalk.Domain.BackOffice.Exception.Menssage;

namespace DevTalk.Test.Domain.BackOffice.Entities;

public class MessageTest
{
    private const string TitleInvalid = "te";
    private const string TitleEmpty = "";
    private const string TitleValid = "Oaljfdlsjfls";
    
    private const string DescriptionInvalid= "te";
    private const string DescriptionEmpty = "";
    private const string DescriptionValid = "djfalçdfak";

    private readonly User UserValid = new User("testeOk", "teste@dja", "teste@gmail.com");
    
    
    [Fact]    
    public void Create_Message_DescriptionInvalid_TitleInvalid_Return_MessageException()
    {
        Assert.Throws<MessageException>(() =>
        {
            var message = new Message(TitleInvalid, UserValid,DescriptionInvalid);
        });
    }
    [Fact]    
    public void Create_Message_DescriptionEmpty_TitleEmpty_Return_MessageException()
    {
        Assert.Throws<MessageException>(() =>
        {
            var message = new Message(TitleEmpty, UserValid,DescriptionEmpty);
        });
    }
    [Fact]    
    public void Create_Message_DescriptionValid_TitleValid_Return_MessageException()
    {
        var message = new Message(TitleValid, UserValid,DescriptionValid);
        Assert.Equal(TitleValid,message.Title);
        Assert.Equal(DescriptionValid,message.Description);
    }
}