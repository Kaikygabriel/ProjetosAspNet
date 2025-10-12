using EduCore.Domain.Entities;
using EduCore.Domain.Interfaces;
using EduCore.Infraestruct.Context;

namespace EduCore.Infraestruct.Repositorys;

public class RepositoryUser  : Repository<User>, IRepositoryUser
{
    public RepositoryUser(AppDbContext context) : base(context)
    {
    }

    public override void Create(User entity)
    {
        entity.PasswordHash = BCrypt.Net.BCrypt.HashPassword(entity.PasswordHash);
        base.Create(entity);
    }
}