using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace FilmesApi.Models;

public class Filme
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    [StringLength(200,MinimumLength = 3)]
    public string? Titulo { get; set; }

    [Required]
    [StringLength(200,MinimumLength = 3)]
    public string? Autor  { get; set; }

    public bool Alugado { get; set; }
}