using EduCore.Domain.Entities;
using EduCore.Domain.Interfaces;
using EduCore.Infraestruct.Context;

namespace EduCore.Infraestruct.Repositorys;

public class RepositoryCourse  : Repository<Course> , IRepositoryCourse
{
    public RepositoryCourse(AppDbContext context) : base(context)
    {
    }
}