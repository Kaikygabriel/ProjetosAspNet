using AlugAI.Domain.Entities;

namespace AlugAI.Domain.ObjectValues;

public class User : Entity
{
    public User() { }
    public User(string name, string password, string? refreshToken, DateTime? expiredRefreshToken)
    {
        Name = name;
        Password = password;
        RefreshToken = refreshToken;
        ExpiredRefreshToken = expiredRefreshToken;
    }

    public string Name { get; }
    public string Password{ get; }
    public string? RefreshToken{ get; set; }
    public DateTime? ExpiredRefreshToken { get; set; }

    public List<String> Roles { get; set; } = new();       
}