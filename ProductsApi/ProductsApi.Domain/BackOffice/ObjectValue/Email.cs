using ProductsApi.Domain.BackOffice.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace ProductsApi.Domain.BackOffice.ObjectValue;

public class Email
{
    protected Email(){ }
    public Email(string address)
    {
        if (string.IsNullOrWhiteSpace(address) || !address.Contains('@'))
            throw new EmailException("Adress in email is invalid");
        Address = address;
    }
    [EmailAddress]
    public string Address { get; set; }
}