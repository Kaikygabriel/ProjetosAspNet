using System.ComponentModel.DataAnnotations;

namespace ApiConsultasMedicas.Model;

public class Consulta
{
    [Key]
    [Required]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(70)]
    public string? NameClient { get; set; }
    
    [Required]
    [MaxLength(70)]
    public string? NameDoctor { get; set; }
    
    [Required]
    [DataType(DataType.Date)]
    public DateTime Date { get; set; }
}