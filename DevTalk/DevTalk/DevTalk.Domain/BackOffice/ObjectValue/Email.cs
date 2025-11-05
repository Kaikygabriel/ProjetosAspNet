using DevTalk.Domain.BackOffice.Exception.Email;

namespace DevTalk.Domain.BackOffice.ObjectValue;

public class Email
{
    protected Email()
    {
        
    }
    public Email(string address)
    {
        if (string.IsNullOrWhiteSpace(address) || !address.Contains('@'))
            throw new EmailException();
        Address = address;
    }

    public string Address { get; set; }
}