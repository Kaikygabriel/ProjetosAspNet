using System.ComponentModel.DataAnnotations;

namespace CatalogoApi.Model;

public class Produto
{
    public Produto() { }

    public Produto(int id, string? nome, string? descricao,
            decimal preco, string? imagemUrl, int estoque, DateTime dataCadastro)
    {
        Id = id;
        Nome = nome;
        Descricao = descricao;
        Preco = preco;
        ImagemUrl = imagemUrl;
        Estoque = estoque;
        DataCadastro = dataCadastro;
    }

    [Key]
    public int Id { get; set; }
    public string? Nome { get; set; }
    public string? Descricao{ get; set; }
    public Decimal Preco{ get; set; }
    public string? ImagemUrl{ get; set; }
    public int Estoque{ get; set; }
    public DateTime DataCadastro{ get; set; }
}

