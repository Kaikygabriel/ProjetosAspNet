using Microsoft.AspNetCore.Identity;

namespace BlibiotecaApi.Model;

public class User  :IdentityUser
{
    public DateTime? ExpiredRefreshToken { get; set; }
    public string? RefreshToken { get; set; }
}