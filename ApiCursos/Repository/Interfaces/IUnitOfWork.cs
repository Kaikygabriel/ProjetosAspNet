using APiCursos.Data;

namespace ApiCursos.Repository.Interfaces;

public interface IUnitOfWork
{
    ApiCursoContext Context { get; }
    IRepositoryCurso RepositoryCurso { get; }
    Task CommitAsync();
    void Dispose();
}