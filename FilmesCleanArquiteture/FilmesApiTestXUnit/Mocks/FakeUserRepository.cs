using System.Linq.Expressions;
using Filmes.Domain.Entities;
using Filmes.Domain.Interfaces;

namespace FilmesApiTestXUnit.Mocks;

public class FakeUserRepository : IRepositoryUser
{
    private List<User> Users = new()
    {
        new User
        {
            Id = 1,
            Name = "Alice Santos",
            Email = "alice.santos@example.com",
            PasswordHash = "$PLACEHOLDER_HASH_1",
            Roles = new List<string> { "User" }
        },
        new User
        {
            Id = 2,
            Name = "Bruno Oliveira",
            Email = "bruno.oliveira@example.com",
            PasswordHash = "$PLACEHOLDER_HASH_2",
            Roles = new List<string> { "User", "Editor" }
        },
        new User
        {
            Id = 3,
            Name = "Camila Pereira",
            Email = "camila.pereira@example.com",
            PasswordHash = "$PLACEHOLDER_HASH_3",
            Roles = new List<string> { "User" }
        },
        new User
        {
            Id = 4,
            Name = "Daniel Costa",
            Email = "daniel.costa@example.com",
            PasswordHash = "$PLACEHOLDER_HASH_4",
            Roles = new List<string> { "User", "Moderator" }
        }
    };
    public async Task<IEnumerable<User>> GetAll(CancellationToken cancellationToken)
    {
        await Task.Delay(0);
        return Users;
    }

    public async Task<User> GetByPredicate(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken)
    {
        await Task.Delay(0);
        return Users.AsQueryable().FirstOrDefault(predicate);
    }

    public void Create(User entity)
    {
        Users.Add(entity);
    }

    public void Update(User entity)
    {
        Users.Add(entity);
    }

    public void Delete(User entity)
    {
        Users.Remove(entity);
    }
}