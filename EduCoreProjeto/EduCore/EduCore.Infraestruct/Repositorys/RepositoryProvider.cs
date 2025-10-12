using EduCore.Domain.Entities;
using EduCore.Domain.Interfaces;
using EduCore.Infraestruct.Context;

namespace EduCore.Infraestruct.Repositorys;

public class RepositoryProvider : Repository<Provider>, IRepositoryProvider
{
    public RepositoryProvider(AppDbContext context) : base(context)
    {
    }
}