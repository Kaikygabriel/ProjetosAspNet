using FilmesApi.Repository.Interfaces;

namespace FilmesApiTestXUnit.Mocks;

public class FakeUnitOfWork : IUnitOfWork
{
    public IFilmeRepository FilmeRepository { get; } = new FakeFilmeRepository();
    public void Commit()
    {
        
    }
}