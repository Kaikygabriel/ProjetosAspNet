using Biblioteca.Domain.BackOffice.Exceptions;

namespace Biblioteca.Domain.ObjectValues;

public class Email
{
    public Email(string adress)
    {
        if (string.IsNullOrEmpty(adress))
            throw new EmailException("Adress in email is null or empty");
        Adress = adress;
    }

    public string Adress  { get; set; }
}