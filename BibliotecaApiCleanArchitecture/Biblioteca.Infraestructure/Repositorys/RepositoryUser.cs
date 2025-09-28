using Biblioteca.Domain.BackOffice.Entities;
using Biblioteca.Domain.BackOffice.Interfaces;
using Biblioteca.Infraestructure.Context;

namespace Biblioteca.Infraestructure.Repositorys;

public class RepositoryUser(AppDbContext context): Repository<User>(context),IRepositoryUser
{
    public override void Create(User entity)
    {
        var hashPassword = BCrypt.Net.BCrypt.HashPassword(entity.Password);
        entity.Password = hashPassword;
        base.Create(entity);
    }
}