using BlibiotecaApi.Repository.Interfaces;

namespace BibliotecaApiTestsXUnit.Mocks;

public class FakeUnitOfwork : IUnitOfWork
{
    public Task Commit()
    {
        throw new NotImplementedException();
    }

    public IBlibiotecaRepository blibiotecaRepository { get; }
}