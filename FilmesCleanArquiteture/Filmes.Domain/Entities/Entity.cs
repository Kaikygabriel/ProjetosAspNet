using System.ComponentModel.DataAnnotations;

namespace Filmes.Domain.Entities;

public abstract class  Entity
{    
    [Key]
    [Required]
    public int Id { get; set; }
}