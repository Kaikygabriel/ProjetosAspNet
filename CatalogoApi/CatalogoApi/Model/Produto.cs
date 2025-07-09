using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using CatalogoApi.Validations;

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
    [PrimeiraLetraMaiuscula]
    public string? Nome { get; set; }
    [Required]
    [StringLength(300, MinimumLength = 3, ErrorMessage = "Descrição é maior que 150 ou menor que 3 caracters")]
    public string? Descricao { get; set; }
    [Required]
    [Column(TypeName = "decimal(10,2)")]
    [Range(1,10000)]
    public Decimal Preco { get; set; }
    [Required]
    [StringLength(300,MinimumLength = 4)]
    [Display(Name = "Imagem url")]
    public string? ImagemUrl { get; set; }
    [Required]
    [Range(0, 10000)]
    public int Estoque { get; set; }
    [Required]
    [Display(Name = "Data Cadastro")]
    [DataType(DataType.DateTime)]
    public DateTime DataCadastro { get; set; }
    public int CategoriaId { get; set; }
    [JsonIgnore]
    public Categoria? Categoria { get; set; }
}

