namespace NotifiMe.Repository.Interface;

public interface IUnitOfWork
{
    Task CommitAsync();
    public IAppointmentRepository AppointmentRepository{ get; }

    public IUserRepository UserRepository { get; }
    public IProviderRepository ProviderRepository { get; }
}