using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogoApi.Model;
[Table("Produtos")]
public class Produto
{

    public Produto() { }


    public Produto(int id, string? nome, string? descricao,
            decimal preco, string? imagemUrl, int estoque, DateTime dataCadastro, int categoriaId, Categoria categoria)
    {
        Id = id;
        Nome = nome;
        Descricao = descricao;
        Preco = preco;
        ImagemUrl = imagemUrl;
        Estoque = estoque;
        DataCadastro = dataCadastro;
        CategoriaId = categoriaId;
        Categoria = categoria;
    }

    [Key]
    public int Id { get; set; }
    [Required]
    [StringLength(70, MinimumLength = 3, ErrorMessage = "Nome é maior que 70 ou menor que 3 caracters")]
    public string? Nome { get; set; }
    [Required]
    [StringLength(300, MinimumLength = 3, ErrorMessage = "Descrição é maior que 150 ou menor que 3 caracters")]
    public string? Descricao { get; set; }
    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public Decimal Preco { get; set; }
    [Required]
    [StringLength(300)]
    [Display(Name = "Imagem url")]
    public string? ImagemUrl { get; set; }
    [Required]
    public int Estoque { get; set; }
    [Required]
    [Display(Name = "Data Cadastro")]
    [DataType(DataType.DateTime)]
    public DateTime DataCadastro { get; set; }
    public int CategoriaId { get; set; }
    public Categoria? Categoria { get; set; }
}

