using System.Linq.Expressions;
using FilmesApi.Models;
using FilmesApi.Pagination;
using FilmesApi.Repository.Interfaces;

namespace FilmesApiTestXUnit.Mocks;

public class FakeFilmeRepository  : IFilmeRepository
{
    List<Filme> ListFilmes =  new List<Filme>
    {
        new Filme { Id = 1, Titulo = "O Senhor dos Anéis: A Sociedade do Anel", Autor = "J.R.R. Tolkien", Alugado = false },
        new Filme { Id = 2, Titulo = "Harry Potter e a Pedra Filosofal", Autor = "J.K. Rowling", Alugado = true },
        new Filme { Id = 3, Titulo = "Matrix", Autor = "Lana Wachowski, Lilly Wachowski", Alugado = false },
        new Filme { Id = 4, Titulo = "Vingadores: Ultimato", Autor = "Stan Lee", Alugado = false },
        new Filme { Id = 5, Titulo = "O Poderoso Chefão", Autor = "Mario Puzo", Alugado = true },
        new Filme { Id = 6, Titulo = "Interestelar", Autor = "Jonathan Nolan, Christopher Nolan", Alugado = false },
        new Filme { Id = 7, Titulo = "Batman: O Cavaleiro das Trevas", Autor = "Christopher Nolan", Alugado = true },
        new Filme { Id = 8, Titulo = "Forrest Gump", Autor = "Winston Groom", Alugado = false },
        new Filme { Id = 9, Titulo = "A Origem", Autor = "Christopher Nolan", Alugado = true },
        new Filme { Id = 10, Titulo = "Clube da Luta", Autor = "Chuck Palahniuk", Alugado = false }
    };
    public IEnumerable<Filme> GetAll()
    {
        return ListFilmes;
    }

    public Filme? GetById(Expression<Func<Filme, bool>> predicate)
    {
        return ListFilmes.AsQueryable().FirstOrDefault(predicate);
    }

    public Filme Created(Filme entity)
    {
        ListFilmes.Add(entity);
        return entity;
    }

    public Filme Update(Filme entity)
    {
        ListFilmes.Add(entity);
        return entity;
    }

    public Filme Delete(Filme entity)
    {
        ListFilmes.Remove(entity);
        return entity;
    }

    public PagedList<Filme> GetAllFilme(FilmePagination pagination)
    {
        return PagedList<Filme>.ToPagedList(ListFilmes,pagination.PageSize,pagination.PageNumber);
    }
}