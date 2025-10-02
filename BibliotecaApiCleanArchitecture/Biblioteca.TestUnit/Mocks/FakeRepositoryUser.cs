using System.Linq.Expressions;
using Biblioteca.Domain.BackOffice.Entities;
using Biblioteca.Domain.BackOffice.Interfaces;
using Biblioteca.Domain.ObjectValues;

namespace Biblioteca.TestUnit.Mocks;

public class FakeRepositoryUser  : IRepositoryUser
{
    public List<User> Users = new()
    {
        new User
        {
            Name = "AnaSilva",
            Password =BCrypt.Net.BCrypt.HashPassword("senha123"),
            Email = new Email("ana.silva@email.com")
        },
        new User
        {
            Name = "Carlos",
            Password = BCrypt.Net.BCrypt.HashPassword("minhasenha"),
            Email = new Email("carlos.pereira@email.com")
        },
        new User
        {
            Name = "Mariana",
            Password = BCrypt.Net.BCrypt.HashPassword("teste456"),
            Email = new Email("mariana.souza@email.com")
        },
        new User
        {
            Name = "Lucas",
            Password = BCrypt.Net.BCrypt.HashPassword("lucas789"),
            Email = new Email("lucas.oliveira@email.com")
        }
    };
    

    public async Task<IEnumerable<User>> GetAll()
    {
        await Task.Delay(0);
        return Users;
    }

    public async Task<User?> GetByPredicate(Expression<Func<User?, bool>> predicate)
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