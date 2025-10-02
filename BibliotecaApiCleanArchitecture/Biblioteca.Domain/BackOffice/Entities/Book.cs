using System.ComponentModel.DataAnnotations;
using Biblioteca.Domain.BackOffice.ObjectValues;

namespace Biblioteca.Domain.BackOffice.Entities;

public class Book : Entity
{
    public Book()
    {
        
    }
    public Book(string title, string author, decimal price)
    {
        Title = title;
        Author = new Author(author);
        Price = price;
    }

    [Required]
    [StringLength(140, MinimumLength = 3)]
    public string Title { get; set; } 
    [Required]
    [StringLength(140,MinimumLength = 3)]
    public Author Author{ get; set; }
    public decimal Price { get; set; }
}