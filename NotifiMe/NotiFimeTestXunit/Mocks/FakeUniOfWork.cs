using NotifiMe.Repository.Interface;

namespace NotiFimeTestXunit.Mocks;

public class FakeUniOfWork : IUnitOfWork
{
    public async Task CommitAsync()
    {
        await Task.Delay(0);
    }

    public IAppointmentRepository AppointmentRepository { get; }
    public IUserRepository UserRepository { get; } = new FakeuserRepository();
    public IProviderRepository ProviderRepository { get; }
}