using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Domain.BackOffice.Entities;

public class Book : Entity
{
    [Required]
    [StringLength(140, MinimumLength = 3)]
    public string Title { get; set; } = string.Empty;
    [Required]
    [StringLength(140,MinimumLength = 3)]
    public string author{ get; set; }=string.Empty;
    public decimal Price { get; set; }
}