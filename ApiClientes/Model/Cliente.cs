using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ApiClientes.Validations;

namespace ApiClientes.Model;

[Table("Clientes")]
public class Cliente
{
    public Cliente()
    {
    }

    public Cliente(int id, string name, bool ativo)
    {
        Id = id;
        Name = name;
        Ativo = ativo;
    }

    [Key]
    [Required]
    public int Id { get; set; }
    [Required]
    [StringLength(100,MinimumLength = 3,ErrorMessage = "Não atingiu o minimu de caracter ou utrapassou o maximo")]
    public string Name { get; set; }
    [Required]
    public bool Ativo { get; set; }
}