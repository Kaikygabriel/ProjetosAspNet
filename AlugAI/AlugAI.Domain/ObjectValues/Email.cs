using System.ComponentModel.DataAnnotations;
using AlugAI.Domain.Exceptions;

namespace AlugAI.Domain.ObjectValues;

public class Email
{
    public Email(string adress)
    {
        if (string.IsNullOrEmpty(adress))
            throw new EmailExceptions("Adress in email is null or empty!");
        Adress = adress;
    }
    [EmailAddress]
    public string Adress { get; set; }
}