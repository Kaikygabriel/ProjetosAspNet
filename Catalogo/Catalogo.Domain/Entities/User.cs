using System.ComponentModel.DataAnnotations;
using Catalogo.Domain.ObjectValue;

namespace Catalogo.Domain.Entities;

public class User : Entity
{
    public User()  { }
    public User(string name, string password, string adress)
    {
        Name = name;
        Password = password;
        Email = new Email(adress);
    }

    [Required]
    [StringLength(140,MinimumLength = 3)]
    public string Name { get; set; } 
    [Required]
    [StringLength(70,MinimumLength = 6)]
    public string Password { get; set; } 
    [Required]
    [EmailAddress]
    public Email Email { get; set; } = null!;
}