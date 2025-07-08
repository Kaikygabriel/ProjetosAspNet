using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BlibiotecaApi.Model;

public class Livro
{
    [Key]
    [Required]
    public int Id { get; set; }
    [Required]
    [StringLength(100,MinimumLength = 3)]
    public string Autor { get; set; }

    [Required]
    [StringLength(300,MinimumLength = 5)]
    public string Titulo { get; set; }
    
    [Required]
    public int BlibiotecaId { get; set; }
    [JsonIgnore]
    public Blibioteca Blibioteca { get; set; }
}