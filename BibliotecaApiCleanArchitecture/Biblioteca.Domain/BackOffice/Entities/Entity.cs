using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Domain.BackOffice.Entities;

public abstract class Entity
{
    [Key]
    [Required]
    public int Id { get; set; }
}