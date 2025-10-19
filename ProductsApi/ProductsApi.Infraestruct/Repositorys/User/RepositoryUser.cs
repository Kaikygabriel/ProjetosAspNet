using ProductsApi.Domain.BackOffice.Interfaces.Users;
using ProductsApi.Infraestruct.Data.Context;

namespace ProductsApi.Infraestruct.Repositorys.User;

public class RepositoryUser : Repository<Domain.BackOffice.Entitys.User>,IRepositoryUser
{
    public RepositoryUser(AppDbContext context) : base(context)
    {
    }

    public override void Create(Domain.BackOffice.Entitys.User entity)
    {
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(entity.Password);
        entity.Password = passwordHash;
        base.Create(entity);
    }
}