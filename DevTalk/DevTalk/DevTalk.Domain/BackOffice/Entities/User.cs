using DevTalk.Domain.BackOffice.Exception.User;
using DevTalk.Domain.BackOffice.ObjectValue;

namespace DevTalk.Domain.BackOffice.Entities;

public class User  :  Entity
{
    protected User(){}
    public User(string name, string password,Email email)
    {
        if (string.IsNullOrWhiteSpace(name) ||
            password.Length < 6)
            throw new UserException();
        Email = email;
        Name = name;
        Password = password;
    }

    public Email Email { get; set; }
    public string Name { get;   set; }
    public string Password { get; set; }
    public List<string>Roles { get; private set; }

    public void SetRole(string role)
        => Roles.Add(role);

    public List<string> GetRoles()
        => Roles;
}