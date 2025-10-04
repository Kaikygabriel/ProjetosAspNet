using System.ComponentModel.DataAnnotations;
using Biblioteca.Domain.BackOffice.Exceptions;
using Biblioteca.Domain.BackOffice.ObjectValues;

namespace Biblioteca.Domain.BackOffice.Entities;

public class User : Entity
{
    public User()
    {
        
    }
    public User(string name, string password, Email email)
    {
        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(password))
            throw new UserException("Arguments in user are invalid.");
        Name = name;
        Password = password;
        Email = email;
    }
    [Required]
    [StringLength(130,MinimumLength = 3)]
    public string Name { get; set; }
    [Required]
    [StringLength(70,MinimumLength = 6)]
    public string Password { get; set; } 
    public  Email Email { get; set; }
    public List<string> Roles { get; set; } = new();

    public string? RefreshToken { get; set; }
    public DateTime? ExpiredRefreshToken { get; set; }
}