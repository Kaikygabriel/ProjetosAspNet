using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Interfaces;
using Biblioteca.Infraestructure.Context;
using Microsoft.EntityFrameworkCore.Internal;

namespace Biblioteca.Infraestructure.Repositoroys;

public class RepositoryUser(AppDbContext context): Repository<User>(context),IRepositoryUser
{
    public override void Create(User entity)
    {
        entity.Password = Net.BCrypt.HashPassword();
        base.Create(entity);
    }
}