namespace FilmesApi.Repository.Interfaces;

public interface IUnitOfWork
{
    public IFilmeRepository FilmeRepository { get;}
    void Commit();
}