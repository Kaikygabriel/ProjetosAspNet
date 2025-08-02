using FilmesApi.Models;
using FilmesApi.Pagination;

namespace FilmesApi.Repository.Interfaces;

public interface IFilmeRepository : IRepository<Filme>
{
    PagedList<Filme> GetAllFilme(FilmePagination pagination);
}