using System.ComponentModel.DataAnnotations;

namespace Filmes.Application.DTOS;

public class RegisterModel
{
    [StringLength(150,MinimumLength = 3)]
    [Required]
    public string Name { get; set; }= string.Empty;
    [StringLength(150,MinimumLength = 3)]
    [Required]
    [EmailAddress]
    public string Email { get; set; }= string.Empty;

    [StringLength(100, MinimumLength = 6)]
    [Required]
    public string Password { get; set; } = string.Empty;
}