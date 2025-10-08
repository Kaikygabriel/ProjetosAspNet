using System.ComponentModel.DataAnnotations;
using EduCore.Domain.ValueObjects;

namespace EduCore.Domain.Entities;

public class User : Entity
{
    public User()
    {
        
    }
    public User(string name, string passwordHash)
    {
        Name = name;
        PasswordHash = passwordHash;
    }
    
    [StringLength(120,MinimumLength = 3)]
    public string Name { get; set; }

    public string PasswordHash { get; set; }

    public string? RefreshToken { get; set; }
    public DateTime? ExpiredRefreshToken { get; set; }

    private List<string> _roles = new();

    public List<string> GetRoles()
        => _roles;
    
    public void  SetRoles(string role)
        => _roles.Add(role);
    
}