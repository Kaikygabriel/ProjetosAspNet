using System.ComponentModel.DataAnnotations;

namespace CategoriasMvc.Models;

public class FilmesViewModel
{
        public int Id { get; set; }
        [Required]
        [StringLength(140,MinimumLength = 3)] 
        public string Titulo { get; set; }= string.Empty;
        [Required]
        [StringLength(140,MinimumLength = 3)] 
        public string Autor { get; set; }= string.Empty;
        [Required]
        [StringLength(140,MinimumLength = 3)] 
        public string Categoria { get; set; }= string.Empty;
}