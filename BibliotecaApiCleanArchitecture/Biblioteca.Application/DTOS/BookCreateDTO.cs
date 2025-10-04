using System.ComponentModel.DataAnnotations;
using Biblioteca.Domain.BackOffice.Entities;

namespace Biblioteca.Application.DTOS;

public class BookCreateDTO
{
    [Required]
    [StringLength(140, MinimumLength = 3)]
    public string Title { get; set; } 
    [Required]
    [StringLength(140,MinimumLength = 3)]
    public Author Author{ get; set; }
    public decimal Price { get; set; }
}