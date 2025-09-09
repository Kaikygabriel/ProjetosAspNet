using AcademyPro.Models;

namespace AcademyPro.Repository.Interfaces;

public interface IUnitOfWOrk
{
    Task CommitAsync();
    public IEnrollmentRepository EnrollmentRepository { get;}
    public ICurseRepository CurseRepository { get;}
}