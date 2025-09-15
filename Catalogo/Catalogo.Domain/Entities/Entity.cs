using System.ComponentModel.DataAnnotations;

namespace Catalogo.Domain.Entities;

public abstract class Entity
{
    [Key]
    public int Id { get;protected set; }
}