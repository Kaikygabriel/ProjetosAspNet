using System.ComponentModel.DataAnnotations;

namespace Filmes.Domain.Entities;

public class Filme : Entity
{
    [Required]
    [StringLength(140,MinimumLength = 3)] 
    public string Titulo { get; set; }
    [Required]
    [StringLength(140,MinimumLength = 3)] 
    public string Autor { get; set; }
    [Required]
    [StringLength(140,MinimumLength = 3)] 
    public string Categoria { get; set; }
}