using Biblioteca.Domain.BackOffice.Entities;
using Biblioteca.Domain.BackOffice.Exceptions;

namespace Biblioteca.Domain.BackOffice.ObjectValues;

public class Email : Entity
{
    public Email(string adress)
    {
        if (string.IsNullOrEmpty(adress))
            throw new EmailException("Adress in email is null or empty");
        Adress = adress;
    }

    public string Adress  { get; set; }
}