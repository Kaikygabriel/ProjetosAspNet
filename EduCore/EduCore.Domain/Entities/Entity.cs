using System.ComponentModel.DataAnnotations;

namespace EduCore.Domain.Entities;

public abstract class Entity
{
    [Key]
    public int Id { get; set; }
}