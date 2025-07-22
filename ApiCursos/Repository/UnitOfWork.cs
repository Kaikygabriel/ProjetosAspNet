using APiCursos.Data;
using ApiCursos.Repository.Interfaces;

namespace ApiCursos.Repository;

public class UnitOfWork : IUnitOfWork
{
    public ApiCursoContext Context { get; }
    private IRepositoryCurso _repositoryCurso;

    public UnitOfWork(ApiCursoContext context)
    {
        Context = context;
    }

    public IRepositoryCurso RepositoryCurso
    {
        get
        {
            return _repositoryCurso = _repositoryCurso ?? new RepositoryCurso(Context);
        }
    }
    public void Commit()
    {
        Context.SaveChanges();
    }

    public void Dispose()
    {
        Context.Dispose();
    }
}