using Microsoft.AspNetCore.Identity;

namespace CatalogoApi.Model;

public class ApplicationUser : IdentityUser
{
    public string? RefreshToeken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
}