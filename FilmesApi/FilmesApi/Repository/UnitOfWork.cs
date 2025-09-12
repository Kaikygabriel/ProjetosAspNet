using FilmesApi.Data;
using FilmesApi.Repository.Interfaces;

namespace FilmesApi.Repository;

public class UnitOfWork : IUnitOfWork
{
    public AppDbContext Context;
    private FilmeRepository _filmeRepository;

    public UnitOfWork(AppDbContext context)
    {
        Context = context;
    }

    public IFilmeRepository FilmeRepository
    {
        get
        {
            return _filmeRepository = _filmeRepository ?? new FilmeRepository(Context);
        }
    }

    public void Commit()
    {
        Context.SaveChanges();
    }
}