using System.Linq.Expressions;
using ProductsApi.Domain.BackOffice.Interfaces.Users;
using ProductsApi.Domain.BackOffice.ObjectValue;

namespace ProductsApi.Test.Mocks;

public class FakeRepositoryUser : IRepositoryUser
{
    private List<User> _users = new List<User>()
    {
        new("kaiky", "teste", new Email("kaiky@gmail.com")),
        new("alves", "teste1", new Email("alves@gmail.com")),
        new("gabriel", "teste2", new Email("gabriel@gmail.com"))
    };
    public Task<IEnumerable<User>> GetAll(QueryStringParameters parameters)
    {
        return Task.FromResult<IEnumerable<User>>(_users);
    }

    public async Task<User?> GetByPredicate(Expression<Func<User, bool>> predicate)
    {
        await Task.Delay(0);
        return _users.AsQueryable().FirstOrDefault(predicate);
    }

    public void Create(User entity)
    {
        if (entity is null)
            throw new Exception();
        _users.Add(entity);
    }

    public void Update(User entity)
    {
        if (entity is null)
            throw new Exception();
        _users.Add(entity);
    }

    public void Delete(User entity)
    {
        if (entity is null)
            throw new Exception();
        _users.Remove(entity);
    }
}