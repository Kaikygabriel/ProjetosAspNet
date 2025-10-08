using System.ComponentModel.DataAnnotations;
using EduCore.Domain.Exceptions;
using EduCore.Domain.Entities;

namespace EduCore.Domain.ValueObjects;

public class Email : Entity
{
    public Email()
    {
    }
    public Email(string adress)
    {
        if (string.IsNullOrEmpty(adress))
            throw new EmailException("Adress in email is null or empty");
        Adress = adress;
    }
    [EmailAddress]
    public string Adress { get; set; }
}