using Barbearia.Domain.BackOffice.Exception;

namespace Barbearia.Domain.BackOffice.ObjectValue;

public class Email
{
    public Email(string address)
    {
        if (string.IsNullOrWhiteSpace(address) || address.Contains('@'))
            throw new EmailException("Address invalid!");
        Address = address;
    }

    public string Address { get; set; }
}