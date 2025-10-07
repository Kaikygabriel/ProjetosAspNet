using System.ComponentModel.DataAnnotations;
using Catalogo.Domain.Entities;
using Catalogo.Domain.Exceptions;

namespace Catalogo.Domain.ObjectValue;

public class Email : Entity 
{
    public Email() {}
    public Email(string adress)
    {
        if (string.IsNullOrEmpty(adress))
            throw new EmailException("Adress in email is null or empty !");
        Adress = adress;
    }

    [EmailAddress] 
    public string Adress { get; set; }
}