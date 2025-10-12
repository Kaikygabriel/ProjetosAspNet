using EduCore.Domain.Entities;
using EduCore.Domain.Interfaces;
using EduCore.Infraestruct.Context;

namespace EduCore.Infraestruct.Repositorys;

public class RepositoryStudent : Repository<Student> ,IRepositoryStudent
{
    public RepositoryStudent(AppDbContext context) : base(context)
    {
    }
}