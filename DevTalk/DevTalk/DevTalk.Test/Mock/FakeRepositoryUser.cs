using System.Linq.Expressions;
using DevTalk.Domain.BackOffice.Entities;
using DevTalk.Domain.BackOffice.Interfaces.User;

namespace DevTalk.Test.Mock;

public class FakeRepositoryUser : IRepositoryUser
{
    private List<User> _users = new()
    {
            new User("João Silva", "joaosilv@", "joao.silva@outlook.com"),
            new User("Fernanda Lima", "f3rnand@", "fernanda.lima@gmail.com"),
            new User("Rafael Alves", "rafael123", "rafael.alves@empresa.com"),
            new User("Camila Rocha", "camiRocha!", "camila.rocha@gmail.com"),
            new User("Pedro Santos", "pedro789", "pedro.santos@hotmail.com"),
            new User("Juliana Oliveira", "juliana1", "juliana.oliveira@outlook.com")
    };
    
    public  Task<IEnumerable<User>> GetAll()
    {
        return Task.FromResult<IEnumerable<User>>(_users);
    }

    public Task<User> GetByPredicate(Expression<Func<User, bool>> predicate)
    {
        return Task.FromResult<User>(_users.AsQueryable().FirstOrDefault(predicate));
    }

    public void Create(User entity)
    {
        if (entity is null)
            throw new ArgumentNullException(nameof(entity));
        _users.Add(entity);
    }

    public void Update(User entity)
    {
        if (entity is null)
            throw new ArgumentNullException(nameof(entity));
        _users.Add(entity);
    }

    public void Delete(User entity)
    {
        if (entity is null)
            throw new ArgumentNullException(nameof(entity));
        _users.Remove(entity);
    }
}