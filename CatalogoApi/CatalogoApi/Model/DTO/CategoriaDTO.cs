using System.ComponentModel.DataAnnotations;

namespace CatalogoApi.Model.Dto
{
    public class CategoriaDTO
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(70, MinimumLength = 3, ErrorMessage = "Caracters ultrapassou 70 ou é menor que 3")]
        public string? Nome { get; set; }
        [Required]
        [StringLength(300)]
        public string? ImageUrl { get; set; }
    }
}
