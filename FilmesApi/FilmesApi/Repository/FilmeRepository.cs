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

    public PagedList<Filme> GetAllFilme(FilmePagination pagination)
    {
        if (pagination.PageNumber == 0)
            pagination.PageNumber = 1;
        var filmes = context.Filmes.AsNoTracking().OrderBy(x => x.Id).ToList();
        return PagedList<Filme>.ToPagedList(filmes,pagination.PageSize,pagination.PageNumber);
    }
}