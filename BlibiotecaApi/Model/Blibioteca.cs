using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BlibiotecaApi.Model;

public class Blibioteca
{
    [Key]
    [Required]
    public int Id { get; set; }
    [StringLength(300)]
    [Required]
    public string Name { get; set; }
    [Required]
    public int Cep { get; set; }
    [JsonIgnore]
    public List<Livro>Livros { get; set; }
}