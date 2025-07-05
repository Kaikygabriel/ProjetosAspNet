using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiCursos.Model;
[Table("Cursos")]
public class Curso
{
    [Key]
    [Required]
    public int Id { get; set; }
    
    [Required]
    [StringLength(200,MinimumLength = 10)] 
    public string Titulo{ get; set; }
    
    [Required]
    [Column(TypeName = "decimal(10,2)")] 
    public decimal Price{ get; set; }
}