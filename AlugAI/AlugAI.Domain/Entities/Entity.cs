using System.ComponentModel.DataAnnotations;

namespace AlugAI.Domain.Entities;

public abstract class Entity
{
    [Key]
    [Required]
    public int Id { get; set; }
}