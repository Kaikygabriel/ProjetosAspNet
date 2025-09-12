using System.ComponentModel.DataAnnotations;

namespace BlibiotecaApi.Model;

public class Livro
{
    [Required]
    [Key]
    public int Id{ get; set; }
    [Required]
    [StringLength(120,MinimumLength = 5)]
    public string Titulo { get; set; }
    [Required]
    [StringLength(100,MinimumLength = 3)]
    public string Autor { get; set; }
    [Required]
    public int BlibiotecaID{ get; set; }

    public Blibioteca Blibioteca { get; set; }
}