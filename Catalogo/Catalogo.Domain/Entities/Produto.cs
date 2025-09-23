using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Catalogo.Domain.Entities;

public class Produto : Entity
{
    public Produto()
    {
        
    }
    public Produto(int id,string name, string descricao, string imagemUrl, int estoque, decimal preco)
    {
        Id = id;
        Nome = name;
        Descricao = descricao;
        ImagemUrl = imagemUrl;
        Estoque = estoque;
        Preco = preco;
    }

    [NotNull]
    [Required]
    [StringLength(200,MinimumLength = 3)]
    public string Nome { get;private set; }
    [Required]
    [StringLength(200,MinimumLength = 3)]
    public string Descricao { get;private set; }
    [Required]
    [StringLength(200,MinimumLength = 3)]
    public string ImagemUrl{ get;private set; }
    public int Estoque { get;private set; }
    public decimal Preco { get; private set; }
    
}