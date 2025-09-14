using System.Linq.Expressions;
using NotifiMe.Models;
using NotifiMe.Repository.Interface;

namespace NotiFimeTestXunit.Mocks;

public class FakeuserRepository  : IUserRepository
{
    public List<User> Users = new List<User>
    {
        new User
        {
            Id = 1,
            Name = "João Silva",
            Email = "joao.silva@example.com", 
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("fjas9832r3h")

        },
        new User
        {
            Id = 2,
            Name = "Mariana Oliveira",
            Email = "mariana.oliveira@example.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("fajdfsklaçjfaçs8jfpjas")

        },
        new User
        {
            Id = 3,
            Name = "Carlos Pereira",
            Email = "carlos.pereira@example.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("hash_carlos_789")
        }
    };
    public async Task<IEnumerable<User>> GetAllAsync()
    {
        await Task.Delay(0);
        return Users;
    }

    public async Task<User?> GetByPredicateAsync(Expression<Func<User, bool>> predicate)
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