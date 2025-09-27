using System.ComponentModel.DataAnnotations;

namespace Filmes.Application.DTOS;

public class CreateFilmeDTO
{
    [Required]
    [StringLength(140, MinimumLength = 3)]
    public string Titulo { get; set; } = string.Empty;
    [Required]
    [StringLength(140,MinimumLength = 3)] 
    public string Autor { get; set; }= string.Empty;
    [Required]
    [StringLength(140,MinimumLength = 3)] 
    public string Categoria { get; set; }= string.Empty;
}