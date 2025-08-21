using Microsoft.AspNetCore.Identity;

namespace ApiClientes.Model;

public class ApplicationUser  : IdentityUser
{
    public DateTime? RefreshTokenExpired { get; set; }
    public string? RefreshToken { get; set; }
}