using APiCursos.Data;

namespace ApiCursos.Repository.Interfaces;

public interface IUnitOfWork
{
    IRepositoryCurso RepositoryCurso { get; }
    Task CommitAsync();
}