using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.VisualBasic;

namespace CatalogoApi.Model;
[Table("Categorias")]
public class Categoria
{
    public Categoria() { }

    public Categoria(int id, string? nome, string? imageUrl)
    {
        Id = id;
        Nome = nome;
        ImageUrl = imageUrl;
        Produtos = new Collection<Produto>();
    }

    [Key]
    public int Id { get; set; }
    [Required]
    [StringLength(70,MinimumLength =3,ErrorMessage ="Caracters ultrapassou 70 ou é menor que 3")]
    public string? Nome { get; set; }
    [Required]
    [StringLength(300)]
    public string? ImageUrl { get; set; }
    public ICollection<Produto>? Produtos { get; set; } 
}

