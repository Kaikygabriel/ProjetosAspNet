using System.ComponentModel.DataAnnotations;

namespace ApiCompras.Model.DTO;

public class VendaDto
{
    [Required]
    [StringLength(150,MinimumLength = 3)]
    [Display(Name = "Nome do Cliente")]
    public string? NomeCliente { get; set; }
    [Required]
    [StringLength(150,MinimumLength = 3)]
    public string? NomeProduto{ get; set; }
}