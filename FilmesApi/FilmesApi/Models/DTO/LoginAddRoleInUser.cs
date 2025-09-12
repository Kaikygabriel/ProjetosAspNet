using System.Reflection.Metadata.Ecma335;

namespace FilmesApi.Models.DTO;

public class LoginAddRoleInUser
{
    public string Role { get; set; }
    public string UserName { get; set; }
}