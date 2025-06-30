using System.ComponentModel.DataAnnotations;

namespace CatalogoApi.Model;
public class Categoria
{
    public Categoria() { }

    public Categoria(int id, string? nome, string? imageUrl)
    {
        Id = id;
        Nome = nome;
        ImageUrl = imageUrl;
    }

    [Key]
    public int Id { get; set; }
    public string? Nome { get; set; }
    public string? ImageUrl { get; set; }
}

