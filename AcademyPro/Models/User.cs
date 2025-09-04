using Microsoft.AspNetCore.Identity;

namespace AcademyPro.Models;

public class User : IdentityUser
{
    public string? RefreshToken { get; set; }
    public DateTime ExpiredRefreshToken { get; set; }

    public List<Curse> IdCurses { get; set; } = new List<Curse>();
}