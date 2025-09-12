using System.ComponentModel.DataAnnotations;

namespace FilmesApi.Models.DTO;

public class FilmesDTO
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
}