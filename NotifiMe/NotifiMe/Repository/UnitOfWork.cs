using NotifiMe.Data;
using NotifiMe.Repository.Interface;

namespace NotifiMe.Repository;

public class UnitOfWork : IUnitOfWork
{
    private AppDbContext context;
    private IUserRepository _repositoryUser;
    private IProviderRepository _repositoryProvider;
    private IAppointmentRepository _appointmentRepository;
    public UnitOfWork(AppDbContext context)
    {
        this.context = context;
    }

    public async Task CommitAsync()
    {
        await context.SaveChangesAsync();
    }

    public IAppointmentRepository AppointmentRepository
    {
        get
        {
            return _appointmentRepository = _appointmentRepository ?? new RepositoryAppointment(context);
        }
    }

    public IUserRepository UserRepository
    {
        get
        {
            return _repositoryUser = _repositoryUser ?? new RepositoryUser(context);
        }
    }

    public IProviderRepository ProviderRepository
    {
        get
        {
            return _repositoryProvider = _repositoryProvider ?? new RepositoryProvider(context);
        }
    }
}