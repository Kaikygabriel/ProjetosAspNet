using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CatalogoApi.Validations;

namespace CatalogoApi.Model.Dto
{
    public class ProdutoDTO
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(70, MinimumLength = 3, ErrorMessage = "Nome é maior que 70 ou menor que 3 caracters")]
        [PrimeiraLetraMaiuscula]
        public string? Nome { get; set; }
        [Required]
        [StringLength(300, MinimumLength = 3, ErrorMessage = "Descrição é maior que 150 ou menor que 3 caracters")]
        public string? Descricao { get; set; }
        [Required]
        [Column(TypeName = "decimal(10,2)")]
        [Range(1, 10000)]
        public Decimal Preco { get; set; }
        [Required]
        [StringLength(300, MinimumLength = 4)]
        [Display(Name = "Imagem url")]
        public string? ImagemUrl { get; set; }
        [Required]
        [Range(0, 10000)]
        public int CategoriaId{get; set;}
    }
}
