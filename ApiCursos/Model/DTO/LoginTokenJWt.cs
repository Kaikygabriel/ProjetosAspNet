using Microsoft.AspNetCore.Identity;

namespace APiCursos.Model.DTO;

public class LoginTokenJWt : IdentityUser
{
    public string? Token { get; set; }
    public string? RefreshToken { get; set; }
}