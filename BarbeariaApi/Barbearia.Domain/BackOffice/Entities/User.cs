using Barbearia.Domain.BackOffice.Exception;
using Barbearia.Domain.BackOffice.ObjectValue;

namespace Barbearia.Domain.BackOffice.Entities;

public class User : Entity
{
    public User(string name, string password, Email email)
    {
        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(name) ||
            name.Length < 3 || password.Length < 3)
            throw new UserException("Parameters in constructor invalid");
        Name = name;
        Password = password;
        Email = email;
    }

    public string Name { get; set; }
    public string Password { get; set; }
    public Email Email { get; set; }

    private List<Role> _roles = new();

    public void AddRole(Role role)
        => _roles.Add(role);
    public IEnumerable<Role> GetRole()
        => _roles;
}