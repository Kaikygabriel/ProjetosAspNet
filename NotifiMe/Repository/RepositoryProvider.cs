using NotifiMe.Data;
using NotifiMe.Models;
using NotifiMe.Repository.Interface;

namespace NotifiMe.Repository;

public class RepositoryProvider(AppDbContext context) : Repository<Provider>(context), IProviderRepository
{
    public override void Create(Provider entity)
    {
        var passwordHashCreate = BCrypt.Net.BCrypt.HashPassword(entity.PasswordHash);
        entity.PasswordHash = passwordHashCreate;
        base.Create(entity);
    }
}