using DevTalk.Domain.BackOffice.Exception.User;
using DevTalk.Domain.BackOffice.ObjectValue;

namespace DevTalk.Domain.BackOffice.Entities;

public class User  :  Entity
{
    public User(){}
    public User(string name, string password,string address)
    {
        if (string.IsNullOrWhiteSpace(name) ||
            password.Length < 6)
            throw new UserException();
        Email = new Email(address);
        Name = name;
        Password = password;
    }

    public Email Email { get; set; }
    public string Name { get;   set; }
    public string Password { get; set; }
    
    
    public List<Message>Messages { get; private set; } = new();

    public List<string> Roles { get; private set; } = new();

    public void SetRole(string role)
        => Roles.Add(role);

    public List<string> GetRoles()
        => Roles;
    public void SetMessage(Message message)
        => Messages.Add(message);

    public List<Message> GetMessages()
        => Messages;
    
    public bool CheckPassword(string password)
        => BCrypt.Net.BCrypt.Verify(password, Password);
}