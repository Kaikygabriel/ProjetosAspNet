using FilmesApi.Data;
using FilmesApi.Models;
using FilmesApi.Pagination;
using FilmesApi.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FilmesApi.Repository;

public class FilmeRepository : Repository<Filme>,IFilmeRepository
{
    public FilmeRepository(AppDbContext context) : base(context)
    {
    }

    public IEnumerable<Filme> GetAllFilme(FilmePagination pagination)
    {
        if (pagination.PageNumber == 0)
            pagination.PageNumber = 1;
        return context.Filmes.AsNoTracking()
            .OrderBy(x => x.Id)
            .Skip((pagination.PageNumber - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ToList();
    }
}