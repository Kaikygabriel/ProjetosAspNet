using AlugAI.Domain.ObjectValues;

namespace AlugAI.Domain.Entities;

public class Provider : Entity
{
    public Provider(){}
    public Provider(Email email, User user)
    {
        Email = email;
        User = user;
    }

    public Email Email { get; set; }
    public User User { get; set; }
    public List<RentalHouse> Houses { get; set; } = new();
}