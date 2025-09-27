using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Domain.Entities;

public class User : Entity
{
    [Required]
    [StringLength(130,MinimumLength = 3)]
    public string Name { get; set; }= string.Empty;
    [Required]
    [StringLength(70,MinimumLength = 6)]
    public string Password { get; set; } = string.Empty;
    public List<string> Roles { get; set; } = new();
}