using Microsoft.AspNetCore.Identity;

namespace Filmes.Infraestruture.Identity;

public class User : IdentityUser
{
    public string? RefreshToken { get; set; }
    public DateTime? ExpiredRefreshToken { get; set; }
}