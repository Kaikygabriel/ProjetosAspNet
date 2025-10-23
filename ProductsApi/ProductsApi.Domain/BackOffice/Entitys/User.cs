using System.ComponentModel.DataAnnotations;
using ProductsApi.Domain.BackOffice.Exceptions;
using ProductsApi.Domain.BackOffice.Interfaces;
using ProductsApi.Domain.BackOffice.ObjectValue;

namespace ProductsApi.Domain.BackOffice.Entitys;

public class User : Entity , IAggregateRoot
{
    protected User(){}

    public User(string name, string password, Email email)
    {
        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(password))
            throw new UserException( );
        Name = name;
        Password = password;
        Email = email;
    }

    public  string Name { get; set; }
    
    [MinLength(3)]
    public string Password { get; set; }
    public Email Email { get; set; }
    
    public string? RefreshToken { get; set; }
    public DateTime? ExpiredRefreshToken { get; set; }

    public List<string> Roles { get; private set; } = new();

    public void SetRoles(string role)
        => Roles.Add(role);

    public List<string> GetRoles()
        => Roles;

    public bool CheckPassword(string passwordVerific)
        => BCrypt.Net.BCrypt.Verify(passwordVerific, this.Password);
}