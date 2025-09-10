using APiCursos.Data;
using ApiCursos.Repository.Interfaces;

namespace ApiCursosTestXUnit.Mocks;

public class FakeUnitOfWork : IUnitOfWork
{
    public IRepositoryCurso RepositoryCurso { get; } = new FakeRepositoryCurso();
    public async Task CommitAsync()
    {
        await Task.Delay(0);
        //[...]
    }
}