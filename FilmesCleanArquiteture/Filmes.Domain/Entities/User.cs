using System.ComponentModel.DataAnnotations;
using Filmes.Domain.ObjectValue;

namespace Filmes.Domain.Entities;

public class User : Entity
{
    public User(){}
    public User(string name, string email, string passwordHash)
    {
        Name = name;
        Email = new Email(email);
        PasswordHash = passwordHash;
    }

    [StringLength(150,MinimumLength = 3)]
    [Required]
    public string Name { get; set; }
    [StringLength(150,MinimumLength = 3)]
    [Required]
    
    public Email Email { get; set; }
    [StringLength(100,MinimumLength = 6)]
    [Required]
    public string PasswordHash { get; set; }

    public string? RefreshToken { get; set; }
    public DateTime? ExpiredRefreshToken { get; set; }
    public List<string>? Roles { get; set; } = new();
}