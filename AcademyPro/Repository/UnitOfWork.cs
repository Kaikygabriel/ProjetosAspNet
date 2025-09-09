using AcademyPro.Data;
using AcademyPro.Repository.Interfaces;

namespace AcademyPro.Repository;

public class UnitOfWork : IUnitOfWOrk
{

    private ICurseRepository _curseRepository;
    private IEnrollmentRepository _enrollmentRepository;
    private readonly AppDbContext context;

    public UnitOfWork(AppDbContext context)
    {
        this.context = context;
    }

    public IEnrollmentRepository EnrollmentRepository
    {
        get
        {
            return _enrollmentRepository = _enrollmentRepository ?? new EnrollmentRepository(context); 
        }
    }

    public ICurseRepository CurseRepository
    {
        get
        {
            return _curseRepository = _curseRepository ?? new CurseRepository(context); 
        }
    }

    public async Task CommitAsync()
    {
        await context.SaveChangesAsync();
    }
}