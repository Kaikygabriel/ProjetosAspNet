using ProductsApi.Domain.BackOffice.Interfaces.Users;
using ProductsApi.Infraestruct.Data.Context;

namespace ProductsApi.Infraestruct.Repositorys.User;

public class RepositoryUser : Repository<Domain.BackOffice.Entitys.User>,IRepositoryUser
{
    public RepositoryUser(AppDbContext context) : base(context)
    {
    }
}