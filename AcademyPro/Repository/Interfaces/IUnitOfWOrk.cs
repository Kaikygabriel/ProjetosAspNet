using AcademyPro.Models;

namespace AcademyPro.Repository.Interfaces;

public interface IUnitOfWOrk
{
    Task Commit();
    public IEnrollmentRepository EnrollmentRepository { get;}
    public ICurseRepository CurseRepository { get;}
}