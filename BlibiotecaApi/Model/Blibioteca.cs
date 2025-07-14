using System.ComponentModel.DataAnnotations;

namespace BlibiotecaApi.Model;

public class Blibioteca
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    [StringLength(150,MinimumLength = 10)]
    public string Name { get; set; }

    public List<Livro>Livros { get; set; }
}