using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Domain.Entities;

public class Entity
{
    [Key]
    [Required]
    public int Id { get; set; }
}