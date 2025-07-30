using FilmesApi.Models;
using FilmesApi.Pagination;

namespace FilmesApi.Repository.Interfaces;

public interface IFilmeRepository : IRepository<Filme>
{
    IEnumerable<Filme> GetAllFilme(FilmePagination pagination);
}