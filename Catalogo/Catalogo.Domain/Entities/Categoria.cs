using System.ComponentModel.DataAnnotations;

namespace Catalogo.Domain.Entities;

public class Categoria : Entity
{
    public Categoria()
    {
        
    }
    public Categoria(string name, string imagemUrl, ICollection<Produto> produtos)
    {
        Nome = name;
        ImagemUrl = imagemUrl;
        Produtos = produtos;
    }
    [Required]
    [StringLength(200,MinimumLength = 3)]
    public string Nome { get; set; }
    [Required]
    [StringLength(200,MinimumLength = 3)]
    public string ImagemUrl { get; set; }
    public ICollection<Produto>Produtos { get; set; }
}