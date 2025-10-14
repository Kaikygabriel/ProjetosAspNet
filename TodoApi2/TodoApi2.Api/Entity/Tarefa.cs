using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApi2.Api.Entity;

[Table("Tarefas")]
public class Tarefa
{
    [Key]
    public int  Id { get; set; }

    [StringLength(100)] 
    public string Title { get; set; } = string.Empty;
}