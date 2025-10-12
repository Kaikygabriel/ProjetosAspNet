using System.Linq.Expressions;
using EduCore.Domain.Entities;
using EduCore.Domain.Interfaces;

namespace EduCore.Test.Mocks;

public class MockUserRepository : IRepositoryUser
{
    private readonly List<User> _users = new();

    public MockUserRepository()
    {
        _users.Add(new User { Name = "Kaiky", PasswordHash = "senhaSegura2" });
        _users.Add(new User { Name = "Maria", PasswordHash = "senhaSegura" });
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        await Task.Delay(0);
        return _users;
    }

    public async Task<User?> GetByPredicateAsync(Expression<Func<User, bool>> predicate)
    {
        await Task.Delay(0);
        return _users.AsQueryable().FirstOrDefault(predicate);
    }

    public void Create(User entity)
    {
        _users.Add(entity);
    }

    public void Update(User entity)
    {
        _users.Add(entity);
    }

    public void Delete(User entity)
    {
        _users.Remove(entity);
    }

}