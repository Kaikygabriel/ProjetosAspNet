using System.ComponentModel.DataAnnotations;

namespace Filmes.Domain.Entities;

public class User : Entity
{
    [StringLength(150,MinimumLength = 3)]
    [Required]
    public string Name { get; set; }
    [StringLength(150,MinimumLength = 3)]
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [StringLength(100,MinimumLength = 6)]
    [Required]
    public string PasswordHash { get; set; }

    public string? RefreshToken { get; set; }
    public DateTime? ExpiredRefreshToken { get; set; }
    public List<string>? Roles { get; set; } = new();
}