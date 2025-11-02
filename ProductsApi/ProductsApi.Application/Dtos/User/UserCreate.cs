using System.ComponentModel.DataAnnotations;
using ProductsApi.Domain.BackOffice.ObjectValue;

namespace ProductsApi.Application.Dtos.User;

public class UserCreate
{
    [Required]
    [StringLength(100,MinimumLength = 3)]
    public string Name { get; set; } = string.Empty;
    [Required]
    [StringLength(70,MinimumLength = 4)]
    public string Password { get; set; }= string.Empty;
    [Required]
    [EmailAddress]
    public string Email { get; set; }= string.Empty;

    public Domain.BackOffice.Entities.User ToUser()
        => new Domain.BackOffice.Entities.User(Name,Password,new Email(Email));
}