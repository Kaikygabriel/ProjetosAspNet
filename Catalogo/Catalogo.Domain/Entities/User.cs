using System.ComponentModel.DataAnnotations;

namespace Catalogo.Domain.Entities;

public class User : Entity
{
    [Required]
    [StringLength(140,MinimumLength = 3)]
    public string Name { get; set; } = string.Empty;
    [Required]
    [StringLength(70,MinimumLength = 6)]
    public string Password { get; set; } = string.Empty;
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
}