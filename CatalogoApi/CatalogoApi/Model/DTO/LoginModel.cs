using System.ComponentModel.DataAnnotations;

namespace CatalogoApi.Model.DTO;

public class  LoginModel
{
 [Required]
 public string? UserName { get; set; }
 
 [Required]
 public string? Password{ get; set; }
}