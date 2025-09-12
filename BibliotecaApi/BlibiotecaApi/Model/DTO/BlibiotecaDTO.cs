using System.ComponentModel.DataAnnotations;

namespace BlibiotecaApi.Model.DTO;

public class BlibiotecaDTO
{
    [Required]
    [StringLength(150,MinimumLength = 10)]
    public string Name { get; set; }

    public List<Livro>? Livros { get; set; }
}