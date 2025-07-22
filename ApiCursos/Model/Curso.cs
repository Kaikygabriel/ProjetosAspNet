using System.ComponentModel.DataAnnotations;

namespace APiCursos.Model;

public class Curso
{
    [Key]
    [Required]
    public int Id { get; set; }
    
    [Required]
    [StringLength(150,MinimumLength = 5)]
    public string Titulo { get; set; }
    [Required]
    [StringLength(150,MinimumLength = 5)]
    public string Autor { get; set; }
}