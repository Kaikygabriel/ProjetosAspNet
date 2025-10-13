using System.ComponentModel.DataAnnotations;

namespace LojaApi.Domain.BackOffice.Entitys;

public abstract class  Entity  
{
    [Key]
    public int Id { get; set; }
}