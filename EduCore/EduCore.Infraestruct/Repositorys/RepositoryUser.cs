using EduCore.Domain.Entities;
using EduCore.Domain.Interfaces;
using EduCore.Infraestruct.Context;

namespace EduCore.Infraestruct.Repositorys;

public class RepositoryUser  : Repository<User>, IRepositoryUser
{
    public RepositoryUser(AppDbContext context) : base(context)
    {
    }
}