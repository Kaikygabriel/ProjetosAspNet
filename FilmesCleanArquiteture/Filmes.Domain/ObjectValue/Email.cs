using System.ComponentModel.DataAnnotations;
using Filmes.Domain.Exceptions;

namespace Filmes.Domain.ObjectValue;

public class Email
{
    public Email(string adress)
    {
        if (string.IsNullOrEmpty(adress))
            throw new EmailException("Adress in email is null or empty.");
        Adress = adress;
    }
    [EmailAddress]
    public string Adress { get; set; }
}