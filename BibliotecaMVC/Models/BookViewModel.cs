using System.ComponentModel.DataAnnotations;

namespace BibliotecaMVC.Models;

public class BookViewModel
{
    public BookViewModel() { }
    public BookViewModel(string title, decimal price)
    {
        Title = title;
        Price = price;
    }

    [Required]
    [StringLength(140, MinimumLength = 3)]
    public string Title { get; set; } 
    [Required]
    [StringLength(140,MinimumLength = 3)]
    public decimal Price { get; set; }
}