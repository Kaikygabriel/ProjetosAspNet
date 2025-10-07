using AlugAI.Domain.ObjectValues;

namespace AlugAI.Domain.Entities;

public class Consumer : Entity
{
    public Consumer(Email email, User user)
    {
        Email = email;
        User = user;
    }

    public Consumer()
    {
    }
    public Email Email { get; set; }
    public User User { get; set; }
}