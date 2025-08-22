using Microsoft.AspNetCore.Identity;

namespace ApiConsultasMedicas.Models;

public class User : IdentityUser
{
    public string? RefreshToken { get; set; }
    public DateTime? ExpiredRefreshToken { get; set; }
}