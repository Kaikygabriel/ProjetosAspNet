using FilmesApi.Data;
using FilmesApi.Models;
using FilmesApi.Repository.Interfaces;

namespace FilmesApi.Repository;

public class FilmeRepository : Repository<Filme>,IFilmeRepository
{
    public FilmeRepository(AppDbContext context) : base(context)
    {
    }
}