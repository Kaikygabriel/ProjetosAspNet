using NotifiMe.Data;
using NotifiMe.Models;
using NotifiMe.Repository.Interface;

namespace NotifiMe.Repository;

public class RepositoryUser(AppDbContext context) : Repository<User>(context), IUserRepository
{
    public override void Create(User entity)
    {
        var passwordHashCreate = BCrypt.Net.BCrypt.HashPassword(entity.PasswordHash);
        entity.PasswordHash = passwordHashCreate;
        base.Create(entity);
    }
}