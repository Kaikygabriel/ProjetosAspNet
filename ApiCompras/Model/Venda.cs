using System.ComponentModel.DataAnnotations;

namespace ApiCompras.Model;

public class Venda
{
    [Key]
    [Required]
    public int Id { get; set; }
    [Required]
    [StringLength(150,MinimumLength = 3)]
    [Display(Name = "Nome do Cliente")]
    public string? NomeCliente { get; set; }
    [Required]
    [StringLength(150,MinimumLength = 3)]
    [Display(Name = "Nome do Produto")]
    public string? NomeProduto{ get; set; }
}